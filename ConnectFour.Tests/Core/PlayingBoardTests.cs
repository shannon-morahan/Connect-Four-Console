using System.Linq;
using ConnectFour.Core;
using ConnectFour.GameOverConditions;
using ConnectFour.Pieces;
using NUnit.Framework;

namespace ConnectFour.Tests.Core
{
    [TestFixture]
    public class PlayingBoardTests
    {
        /// <summary>
        /// Does PlayingBoard initialise the Game Over condition types as expected?
        /// </summary>
        [Test]
        public void PlayingBoardInitialisesGameOverTypesCorrectly()
        {
            Dimensions dimension = new Dimensions { Rows = 5, Columns = 5 };
            PlayingBoard board = new PlayingBoard(dimension);
            Assert.AreEqual(4, board.GameOverConditionTypes.Count());
            Assert.That(board.GameOverConditionTypes.Contains(typeof(HorizontaGameOverCondition)));
            Assert.That(board.GameOverConditionTypes.Contains(typeof(VerticalGameOverCondition)));
            Assert.That(board.GameOverConditionTypes.Contains(typeof(DiagonalGameOverCondition)));
            Assert.That(board.GameOverConditionTypes.Contains(typeof(DrawGameOverCondition)));
        }

        /// <summary>
        /// Do we have 25 empty pieces when we initialse the board with 5 rows and 5 columns?
        /// </summary>
        [Test]
        public void PlayingBoardInitialisesWithEmptyPieces()
        {
            Dimensions dimension = new Dimensions { Rows = 5, Columns = 5 };
            PlayingBoard board = new PlayingBoard(dimension);

            // Can use Linq for this, but this is clearer IMO
            for (int i = 0; i < dimension.Rows; i++)
            {
                for (int j = 0; j < dimension.Columns; j++)
                {
                    Assert.That(board.Pieces[i, j] is EmptyPiece);
                }
            }
        }
        
        /// <summary>
        /// Do we get an InvalidMove event when we try to place a piece at column "-1"?
        /// </summary>
        [Test]
        public void PlayingBoardReportsLowerOutOfBoundsCorrectly()
        {
            Dimensions dimension = new Dimensions { Rows = 5, Columns = 5 };
            PlayingBoard board = new PlayingBoard(dimension);

            board.InvalidMove += delegate
            {
                Assert.Pass("PlayingBoardReportsOutOfBoundsCorrectly recieved InvalidMove");
            };

            board.ValidMove += delegate
            {
                Assert.Fail("PlayingBoardReportsOutOfBoundsCorrectly should not have recieved valid move");
            };

            board.DropPieceIntoColumn(-1);

            Assert.Fail("PlayingBoardReportsOutOfBoundsCorrectly Did not recieve an event");
        }

        /// <summary>
        /// Do we get an InvalidMove event when we try to place a piece at column "6" in a 5x5 board?
        /// </summary>
        [Test]
        public void PlayingBoardReportsUpperOutOfBoundsCorrectly()
        {
            Dimensions dimension = new Dimensions { Rows = 5, Columns = 5 };
            PlayingBoard board = new PlayingBoard(dimension);

            board.InvalidMove += delegate
            {
                Assert.Pass("PlayingBoardReportsOutOfBoundsCorrectly recieved InvalidMove");
            };

            board.ValidMove += delegate
            {
                Assert.Fail("PlayingBoardReportsOutOfBoundsCorrectly should not have recieved valid move");
            };

            board.DropPieceIntoColumn(6);
        }

        /// <summary>
        /// Do we get an InvalidMove event when the column is full and we try to drop a piece?
        /// </summary>
        [Test]
        public void PlayingBoardReportsFullColumnCorrectly()
        {
            Dimensions dimension = new Dimensions { Rows = 5, Columns = 5 };
            PlayingBoard board = new PlayingBoard(dimension);


            // Drop 5 pieces in column 1 - there's only 5 available
            board.DropPieceIntoColumn(1);
            board.DropPieceIntoColumn(1);
            board.DropPieceIntoColumn(1);
            board.DropPieceIntoColumn(1);
            board.DropPieceIntoColumn(1);

            // Now we're going to expect that the sixth piece in column 1 is invalid
            board.InvalidMove += delegate
            {
                Assert.Pass("PlayingBoardReportsOutOfBoundsCorrectly recieved InvalidMove");
            };

            board.DropPieceIntoColumn(1);
        }
    }
}
