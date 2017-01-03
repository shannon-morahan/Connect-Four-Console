using ConnectFour.GameOverConditions;
using ConnectFour.Pieces;
using NUnit.Framework;

namespace ConnectFour.Tests.GameOverConditions
{
    [TestFixture]
    public class VerticalWinConditionTests : GameOverConditionTestBase
    {
        /// <summary>
        /// Does Yellow win for the most basic vertical win in column 0?
        /// </summary>
        [Test]
        public void VerticalWinWorksFromFirstColumnYellow()
        {
            // o o o o o
            // Y o o o o
            // Y o o o R
            // Y o o o R
            // Y o o o R            
            int[] columnMoves = { 0, 4, 0, 4, 0, 4, 0 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 7, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(YellowPiece), expectedGameOverType: typeof(VerticalGameOverCondition));
        }

        /// <summary>
        /// Does Red win for the most basic vertical win in column 0?
        /// </summary>
        [Test]
        public void VerticalWinWorksFromFirstColumnRed()
        {
            // o o o o o
            // R o o o o
            // R o o o Y
            // R o o o Y
            // R o o Y Y      
            int[] columnMoves = { 4, 0, 4, 0, 4, 0, 3, 0 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 8, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(RedPiece), expectedGameOverType: typeof(VerticalGameOverCondition));
        }

        /// <summary>
        /// Does Yellow win for the most basic vertical win the last column?
        /// </summary>
        [Test]
        public void VerticalWinWorksFromLastColumnYellow()
        {
            // o o o o o
            // o o o o Y
            // R o o o Y
            // R o o o Y
            // R o o o Y            
            int[] columnMoves = { 4, 0, 4, 0, 4, 0, 4 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 7, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(YellowPiece), expectedGameOverType: typeof(VerticalGameOverCondition));
        }
        /// <summary>
        /// Does Red win for the most basic vertical win the last column?
        /// </summary>
        [Test]
        public void VerticalWinWorksFromLastColumnRed()
        {
            // o o o o o
            // o o o o R
            // Y o o o R
            // Y o o o R
            // Y Y o o R      
            int[] columnMoves = { 0, 4, 0, 4, 0, 4, 1, 4 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 8, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(RedPiece), expectedGameOverType: typeof(VerticalGameOverCondition));
        }

    }
}
