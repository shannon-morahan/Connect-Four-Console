using ConnectFour.Pieces;

namespace ConnectFour.Core
{
    /// <summary>
    /// Monitors the current player (Yellow or Red)
    /// </summary>
    public class TurnMonitor
    {
        /// <summary>
        /// Current player piece
        /// </summary>
        public IPiece CurrentPlayerPiece = new YellowPiece();

        /// <summary>
        /// Changes from Red to Yellow, or from Yellow to Red, depending on the CurrentPlayerPiece
        /// </summary>
        public void SwapPiece()
        {
            if(CurrentPlayerPiece is YellowPiece)
            {
                CurrentPlayerPiece = new RedPiece();
            }
            else if(CurrentPlayerPiece is RedPiece)
            {
                CurrentPlayerPiece = new YellowPiece();
            }
        }
    }
}
