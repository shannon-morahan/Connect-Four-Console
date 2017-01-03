using ConnectFour.GameOverConditions;
using ConnectFour.Pieces;
using NUnit.Framework;

namespace ConnectFour.Tests.GameOverConditions
{
    [TestFixture]
    public class DiagonalWinConditionTests : GameOverConditionTestBase
    {
        /// <summary>
        /// Does Yellow win with the most basic diagonal left to right win?
        /// </summary>
        [Test]
        public void DiagonalWinWorksForYellowBottomLeftToTopRight()
        {
            // o o o o o
            // o o o Y o
            // o o Y Y o
            // R Y Y R o
            // Y R R R o            
            // 0 1 2 3 4
            int[] columnMoves = { 0, 1, 1, 2, 2, 3, 2, 3, 3, 0, 3 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 11, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(YellowPiece), expectedGameOverType: typeof(DiagonalGameOverCondition));
        }

        /// <summary>
        /// Does Red win with the most basic diagonal left to right win?
        /// </summary>
        [Test]
        public void DiagonalWinWorksForRedBottomLeftToTopRight()
        {
            // o o o o o
            // o o o R o
            // o o R R o
            // Y R R Y o
            // R Y Y Y Y            
            // 0 1 2 3 4
            int[] columnMoves = { 1, 0, 2, 1, 2, 2, 3, 3, 3, 3 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 10, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(RedPiece), expectedGameOverType: typeof(DiagonalGameOverCondition));
        }

        /// <summary>
        /// Does Yellow win with the most basic diagonal right to left win?
        /// </summary>
        [Test]
        public void DiagonalWinWorksForYellowBottomRightToTopLeft()
        {
            // o o o o o
            // o Y o o o
            // o R Y o o
            // o Y R Y o
            // R Y R R Y            
            // 0 1 2 3 4
            int[] columnMoves = { 4, 3, 3, 2, 1, 2, 1, 1, 2, 0, 1 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 11, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(YellowPiece), expectedGameOverType: typeof(DiagonalGameOverCondition));


        }

        /// <summary>
        /// Does Red win with the most basic diagonal right to left win?
        /// </summary>
        [Test]
        public void DiagonalWinWorksForRedBottomRightToTopLeft()
        {
            // o o o o o
            // o R o o o
            // o Y R o o
            // o R Y R o
            // o Y Y Y R            
            // 0 1 2 3 4
            int[] columnMoves = { 3, 4, 2, 3, 2, 2, 1, 1, 1, 1 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 10, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: typeof(RedPiece), expectedGameOverType: typeof(DiagonalGameOverCondition));
        }
    }
}
