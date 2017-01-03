using ConnectFour.Core;

namespace ConnectFour.GameOverConditions
{
    /// <summary>
    /// Interface to allow game over conditions to be checked.
    /// Currently this is VerticalGameOverCondition, HorizontalGameOverCondition, DiagonalGameOverCondition and DrawGameOverCondition
    /// </summary>
    public interface IGameOverCondition
    {
        /// <summary>
        /// Checks if the current board has a game over condition.
        /// </summary>
        /// <param name="board">The current PlayingBoard</param>
        /// <returns>True if there is a game over condition, false otherwise</returns>
        bool IsGameOver(PlayingBoard board);
    }
}
