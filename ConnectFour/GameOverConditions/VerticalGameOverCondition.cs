using System.Diagnostics;
using ConnectFour.Core;
using ConnectFour.Pieces;

namespace ConnectFour.GameOverConditions
{
    /// <summary>
    /// Checks the board for a vertical win.
    /// That is, same colour 4 times in a column.
    /// </summary>
    public class VerticalGameOverCondition : IGameOverCondition
    {
        /// <summary>
        /// Iterates the board and returns true if 4 pieces of the same colour are found in a column
        /// </summary>
        /// <param name="board">The current playing board</param>
        /// <returns>True if 4 of the same colour are found vertically, false otherwise</returns>
        public bool IsGameOver(PlayingBoard board)
        {
            for (int column = 0; column < board.ColumnCount; column++)
            {
                int countOfContiguous = 0;

                IPiece currentPiece = board.TurnMonitor.CurrentPlayerPiece;

                for (int row = 0; row < board.RowCount; row++)
                {
                    IPiece checkPiece = board.Pieces.GetValue(row, column) as IPiece;

                    Debug.Assert(checkPiece != null, "checkPiece != null");
                    if (checkPiece.GetType() == currentPiece.GetType())
                    {
                        countOfContiguous++;
                        if (countOfContiguous == 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        countOfContiguous = 0;
                    }

                }
            }

            return false;
        }
    }
}
