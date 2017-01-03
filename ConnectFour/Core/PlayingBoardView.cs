using ConnectFour.Pieces;
using System;
using System.Diagnostics;
using System.Text;

namespace ConnectFour.Core
{
    /// <summary>
    /// A console representation of the current state of a PlayingBoard.
    /// 
    /// </summary>
    public class PlayingBoardView
    {
        /// <summary>
        /// The Board associated with this View
        /// </summary>
        private readonly PlayingBoard Board;

        /// <summary>
        /// The PlayingBoard to associate with this view
        /// </summary>
        /// <param name="board"></param>
        public PlayingBoardView(PlayingBoard board)
        {
            Board = board;
        }

        /// <summary>
        /// Renders a string represention of the current state of the board.
        /// </summary>
        /// <returns></returns>
        public string RenderAsString()
        {
            StringBuilder builder = new StringBuilder();

            // !!! going down the rows !!!!
            for (int row = Board.RowCount - 1; row > -1; row--)
            {
                for (int column = 0; column < Board.ColumnCount; column++)
                {
                    IPiece checkPiece = Board.Pieces.GetValue(row, column) as IPiece;
                    Debug.Assert(checkPiece != null, "checkPiece != null");
                    builder.Append($"{checkPiece.BoardRepresentationCharacter} ");
                }
                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
