using ConnectFour.Core;
using ConnectFour.Pieces;
using System;

namespace ConnectFour.GameOverConditions
{   
    /// <summary>
    /// Two ways of winning diagonally. North East or North West.
    /// </summary>
    public enum Direction { BottomLeftToTopRight, BottomRightToTopLeft };

    /// <summary>
    /// A IGameOverCondition check for a Diagonal win
    /// </summary>
    public class DiagonalGameOverCondition : IGameOverCondition
    {
        /// <summary>
        /// The Current PlayingBoard being used
        /// </summary>
        private PlayingBoard Board;

        /// <summary>
        /// Implements IGameOverCondition.IsGameOver to check if there is a diagonal win.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsGameOver(PlayingBoard board)
        {
            Board = board;
            return IsThereADiagonalWin();
        }

        /// <summary>
        /// The actual method that checks if there is a diagonal win.
        /// </summary>
        /// <returns>true if the current player has won diagonally, false otherwise</returns>
        private bool IsThereADiagonalWin()
        {
            // Iterate through all the pieces, and check both NE and NW counts for greater than 3 of the same kind.
            for (int row = 0; row < Board.RowCount; row++)
            {
                for (int column = 0; column < Board.ColumnCount; column++)
                {
                    IPiece piece = Board.Pieces[row, column];

                    // If it's empty then there is no need to check
                    if (piece is EmptyPiece) continue;

                    // Check our four directions for win conditions
                    if (CountPiecesOfTypeInDirectionRecursive(Direction.BottomLeftToTopRight, 0, row, column, piece) >= 3 ||
                        CountPiecesOfTypeInDirectionRecursive(Direction.BottomRightToTopLeft, 0, row, column, piece) >= 3)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Counts the number of pieces given a direction that are of the same type a piece.
        /// </summary>
        /// <param name="direction">Either Up and Left, or Up and Right</param>
        /// <param name="currentDepth">Current recursive depth</param>
        /// <param name="row">The current row of the piece that we're starting from</param>
        /// <param name="column">The current column of the piece that we're starting from</param>
        /// <param name="piece">The piece that we're starting our search from</param>
        /// <returns></returns>
        private int CountPiecesOfTypeInDirectionRecursive(Direction direction, int currentDepth, int row, int column, IPiece piece)
        {
            int nextPieceRow;
            int nextPieceColumn;
            bool isNextPieceSameColour = GetNextPieceInDirectionOrEmptyIfOutOfBounds(direction, row, column, out nextPieceRow, out nextPieceColumn).GetType() == piece.GetType();
            if (isNextPieceSameColour)
            {
                currentDepth++;
                return CountPiecesOfTypeInDirectionRecursive(direction, currentDepth, nextPieceRow, nextPieceColumn, piece);
            }
            else
            {
                return currentDepth;
            }
        }

        /// <summary>
        /// Returns the colour (IPiece) of the next piece in a given direction (Up and Right or Up and Left)
        /// </summary>
        /// <param name="direction">Either BottomLeftToTopRight or BottomRightToTopLeft</param>
        /// <param name="row">The current row of the piece that we're going to check the next of</param>
        /// <param name="column">The current column of the piece that we're going to check the next of</param>
        /// <param name="nextPieceRow">The next row of the direction that we want to check</param>
        /// <param name="nextPieceColumn">The next column of the direction we want to check</param>
        /// <returns></returns>
        private IPiece GetNextPieceInDirectionOrEmptyIfOutOfBounds(Direction direction, int row, int column, out int nextPieceRow, out int nextPieceColumn)
        {
            switch (direction)
            {
                case Direction.BottomLeftToTopRight:
                    nextPieceRow = row + 1;
                    nextPieceColumn = column + 1;
                    break;
                case Direction.BottomRightToTopLeft:
                    nextPieceRow = row + 1;
                    nextPieceColumn = column - 1;
                    break;
                default:
                    // http://stackoverflow.com/questions/17324145/how-to-test-or-exclude-private-unreachable-code-from-code-coverage
                    // Not sure how to test this statement without introducting a "Nothing" enum value?
                    // Overkill since we know that can't ever reach this statement now and if a dev changes it, it'll execption
                    throw new NotSupportedException("Given direction is not supported. Have you just updated the Direction enum?");
            }
            return ReturnPieceIfInBoundsOrEmptyIfNot(nextPieceRow, nextPieceColumn);
        }

        /// <summary>
        /// Returns an empty piece if out of bound, otherwise returns the piece at the given row,column combination.
        /// </summary>
        /// <param name="row">A row on the board, may be out of bounds</param>
        /// <param name="column">A column on the board, may be out of bounds</param>
        /// <returns></returns>
        private IPiece ReturnPieceIfInBoundsOrEmptyIfNot(int row, int column)
        {
            if (row < 0 || row >= Board.RowCount) return new EmptyPiece();
            if (column < 0 || column >= Board.ColumnCount) return new EmptyPiece();
            IPiece piece = Board.Pieces[row, column];
            return piece;
        }

    }
}

