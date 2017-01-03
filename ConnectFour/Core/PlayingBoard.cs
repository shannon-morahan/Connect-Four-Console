using ConnectFour.Pieces;
using ConnectFour.GameOverConditions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ConnectFour.Core
{
    public delegate void PlayingBoardInvalidMove(string errorMessage);
    public delegate void PlayingBoardValidMove(bool isGameOver = false, Type gameOverType = null);

    /// <summary>
    /// Represents a playing board of rows and columns with each place having 3 possible pieces (EmptyPiece, YellowPiece, RedPiece).
    /// </summary>
    public class PlayingBoard
    {
        /// <summary>
        /// Fired if out of bounds or full column
        /// </summary>
        public event PlayingBoardInvalidMove InvalidMove;

        /// <summary>
        /// Fired if a valid move was made
        /// </summary>
        public event PlayingBoardValidMove ValidMove;

        /// <summary>
        /// Allows reading (but not setting) of the pieces
        /// </summary>
        public IPiece[,] Pieces { get; }

        /// <summary>
        /// Returns the count of items in the first dimension (i.e. "Rows")
        /// </summary>
        public int RowCount => Pieces.GetLength(0);

        /// <summary>
        /// Returns the count of items in the first dimension (i.e. "Columns")
        /// </summary>
        public int ColumnCount => Pieces.GetLength(1);

        /// <summary>
        /// Yellow, Red, Yellow, Red... ect.
        /// </summary>
        public TurnMonitor TurnMonitor { get; } = new TurnMonitor();

        /// <summary>
        /// Just lists the types in the assembly that implement IWinCondition
        /// </summary>
        public IEnumerable<Type> GameOverConditionTypes { get; }

        /// <summary>
        /// Protected constructor. You must pass dimensions to the board to construct it.
        /// 
        /// Typically for code like this (i.e. empty constructors etc), you can add the 
        /// System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttri‌​bute
        /// </summary>
        [ExcludeFromCodeCoverage]
        // ReSharper disable once UnusedMember.Local
        private PlayingBoard() { }

        /// <summary>
        /// Initalises the playing board with the given dimensions
        /// </summary>
        /// <param name="dimension"></param>
        public PlayingBoard(Dimensions dimension)
        {
            GameOverConditionTypes = GetGameOverConditionTypesFromAssembly();

            Pieces = new IPiece[dimension.Rows, dimension.Columns];

            // Can use Linq for this, but this is clearer IMO
            for (int i = 0; i < dimension.Rows; i++)
            {
                for (int j = 0; j < dimension.Columns; j++)
                {
                    Pieces[i, j] = new EmptyPiece();
                }
            }
        }

        /// <summary>
        /// Just reads all the concrete classes that implement IGameOvercondition in the current assembly
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Type> GetGameOverConditionTypesFromAssembly()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).
            Where(p => typeof(IGameOverCondition).IsAssignableFrom(p) && !p.IsInterface);
        }

        /// <summary>
        /// Tries to drop the next piece at the specified column on the board.
        /// </summary>
        /// <param name="column"></param>
        public void DropPieceIntoColumn(int column)
        {
            // Is the column outside the bounds?
            if (column < 0 || column > (Pieces.GetLength(1) - 1))
            {
                InvalidMove?.Invoke(Properties.Resources.InvalidMove);
                return;
            }

            // How high/tall is the column?
            int verticalBoardHeight = Pieces.GetLength(0);

            // How many existing placed pieces does this column have?
            int existingPiecesInColumn = 0;
            for (int row = 0; row < verticalBoardHeight; row++)
            {
                if (!(Pieces[row, column] is EmptyPiece))
                {
                    existingPiecesInColumn++;
                }
                else
                {
                    break;
                }
            }

            // Is the column full?
            if (existingPiecesInColumn >= verticalBoardHeight)
            {
                InvalidMove?.Invoke(Properties.Resources.InvalidMove);
                return;
            }


            // We're good - drop the piece
            Debug.Print($"Dropping {TurnMonitor.CurrentPlayerPiece.ColorMessage} @ {existingPiecesInColumn},{column}");
            Pieces[existingPiecesInColumn, column] = TurnMonitor.CurrentPlayerPiece;

            // Have we met the win conditions? Horizontal, vertical or on the diagonal?
            foreach (Type conditionType in GameOverConditionTypes)
            {
                if (conditionType.IsInterface == false)
                {
                    IGameOverCondition condition = Activator.CreateInstance(conditionType) as IGameOverCondition;
                    if (condition != null && condition.IsGameOver(this))
                    {                        
                        ValidMove?.Invoke(isGameOver: true, gameOverType: condition.GetType());
                        return;
                    }
                }
            }

            // No, Then move to the next player
            TurnMonitor.SwapPiece();

            // And notify of a correct move
            ValidMove?.Invoke();
        }

    }
}
