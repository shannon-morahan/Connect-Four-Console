using ConnectFour.GameOverConditions;
using NUnit.Framework;

namespace ConnectFour.Tests.GameOverConditions
{
    [TestFixture]
    public class DrawConditionTests : GameOverConditionTestBase
    {
        /// <summary>
        /// Does a Draw occur when we fill the entire board with pieces?
        /// </summary>
        [Test]
        public void DrawConditionWorksFullWithFullBoard()
        {
            // Y R Y R Y
            // R Y R Y R
            // R Y R Y Y
            // R R Y R R
            // Y R Y R Y            
            // 0 1 2 3 4
            int[] columnMoves = { 0, 1, 0, 1, 2, 3, 2, 3, 4, 4, 1, 0, 1, 0, 3, 2, 3, 2, 4, 4, 0, 1, 2, 3, 4 };

            RunGameOverConditionTest(rows: 5, columns: 5,
                expectedValidMoveCount: 25, expectedInvalidMoveCount: 0, expectedGameOverCount: 1,
                columnMoves: columnMoves, expectedWinnerType: null, expectedGameOverType: typeof(DrawGameOverCondition));
        }
    }
}
