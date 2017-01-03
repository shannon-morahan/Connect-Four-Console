using ConnectFour.Pieces;
using System.Linq;
using ConnectFour.Core;

namespace ConnectFour.GameOverConditions
{
    /// <summary>
    /// Checks if there is a "Draw" condition in the given PlayingBoard.
    /// </summary>
    public class DrawGameOverCondition : IGameOverCondition
    {
        /// <summary>
        /// If there are no EmptyPiece spots (i.e. No remaining places to put a piece)
        /// returns true for game over.
        /// </summary>
        /// <param name="board">The current PlayingBoard</param>
        /// <returns></returns>
        public bool IsGameOver(PlayingBoard board)
        {
            return !board.Pieces.OfType<EmptyPiece>().Any();
        }
    }
}
