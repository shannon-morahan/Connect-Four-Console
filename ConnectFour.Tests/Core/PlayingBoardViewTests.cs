using ConnectFour.Core;
using NUnit.Framework;

namespace ConnectFour.Tests.Core
{
    [TestFixture]
    public class PlayingBoardViewTests
    {
        /// <summary>
        /// Does an empty board render as expected?
        /// </summary>
        [Test]
        public void PlayingBoardViewShowsCorrectEmptyBoard()
        {
            PlayingBoard board = new PlayingBoard(new Dimensions { Rows = 5, Columns = 6 });
            PlayingBoardView view = new PlayingBoardView(board);
            string result = view.RenderAsString();
            string expected = @"o o o o o o 
o o o o o o 
o o o o o o 
o o o o o o 
o o o o o o 
";
            Assert.AreEqual(result, expected);
        }

        /// <summary>
        /// Does a board that has had 3 yellow and 2 red pieces dropped on the bottom row render correctly?
        /// </summary>
        [Test]
        public void PlayingBoardViewShows5PiecesHorizontallyCorrectly()
        {
            PlayingBoard board = new PlayingBoard(new Dimensions { Rows = 5, Columns = 6 });
            PlayingBoardView view = new PlayingBoardView(board);

            // Yellow                       // Red
            board.DropPieceIntoColumn(0); board.DropPieceIntoColumn(1);
            board.DropPieceIntoColumn(2); board.DropPieceIntoColumn(3);
            board.DropPieceIntoColumn(4);

            string result = view.RenderAsString();
            string expected = @"o o o o o o 
o o o o o o 
o o o o o o 
o o o o o o 
Y R Y R Y o 
";
            Assert.AreEqual(result, expected);
        }

        /// <summary>
        /// Does a board that has had 3 yellow and 2 red pieces dropped in the first row render correctly?
        /// </summary>
        [Test]
        public void PlayingBoardViewShows5PiecesVerticallyCorrectly()
        {
            PlayingBoard board = new PlayingBoard(new Dimensions { Rows = 5, Columns = 6 });
            PlayingBoardView view = new PlayingBoardView(board);

            // Yellow                       // Red
            board.DropPieceIntoColumn(0); board.DropPieceIntoColumn(0);
            board.DropPieceIntoColumn(0); board.DropPieceIntoColumn(0);
            board.DropPieceIntoColumn(0);

            string result = view.RenderAsString();
            string expected = @"Y o o o o o 
R o o o o o 
Y o o o o o 
R o o o o o 
Y o o o o o 
";
            Assert.AreEqual(result, expected);
        }
    }
}
