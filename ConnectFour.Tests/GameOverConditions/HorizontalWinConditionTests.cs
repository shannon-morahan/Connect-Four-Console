using ConnectFour.GameOverConditions;
using ConnectFour.Pieces;
using NUnit.Framework;

namespace ConnectFour.Tests.GameOverConditions
{
    [TestFixture]
    public class HorizontalWinConditionTests : GameOverConditionTestBase
    {
        /// <summary>
        /// Does Yellow win for the most basic horizontal win from the left?
        /// </summary>
        [Test]
        public void HorizontalWinWorksFromFirstColumnYellow()
        {
            // o o o o o
            // o o o o o
            // o o o o R
            // o o o o R
            // Y Y Y Y R            
            int[] columnMoves = { 0, 4, 1, 4, 2, 4, 3 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 7, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(YellowPiece), expectedGameOverType: typeof(HorizontaGameOverCondition));
        }

        /// <summary>
        /// Does Red win for the most basic horizontal win from the left?
        /// </summary>
        [Test]
        public void HorizontalWinWorksFromFirstColumnRed()
        {
            // o o o o o o
            // o o o o o o
            // o o o o o o
            // o o o o Y Y
            // R R R R Y Y      
            int[] columnMoves = { 4, 0, 4, 1, 5, 2, 5, 3 };

            RunGameOverConditionTest(rows: 5, columns: 6,
                expectedValidMoveCount: 8, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(RedPiece), expectedGameOverType: typeof(HorizontaGameOverCondition));
        }
        /// <summary>
        /// Does Yellow win for the most basic horizontal win from the right?
        /// </summary>
        [Test]
        public void HorizontalWinWorksFromLastColumnYellow()
        {
            // o o o o o
            // o o o o o
            // R o o o o
            // R o o o o
            // R Y Y Y Y
            int[] columnMoves = { 4, 0, 3, 0, 2, 0, 1 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 7, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(YellowPiece), expectedGameOverType: typeof(HorizontaGameOverCondition));
        }

        /// <summary>
        /// Does Red win for the most basic horizontal right?
        /// </summary>
        [Test]
        public void HorizontalWinWorksFromLastColumnRed()
        {
            // o o o o o o
            // o o o o o o
            // Y o o o o o
            // Y o o o o o
            // Y Y R R R R      
            int[] columnMoves = { 0, 5, 0, 4, 0, 3, 1, 2 };

            RunGameOverConditionTest(rows: 5, columns: 6,
                expectedValidMoveCount: 8, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(RedPiece), expectedGameOverType: typeof(HorizontaGameOverCondition));
        }
    }
}
