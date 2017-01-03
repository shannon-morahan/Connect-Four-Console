using ConnectFour.Core;
using ConnectFour.GameOverConditions;
using NUnit.Framework;
using System;

namespace ConnectFour.Tests.GameOverConditions
{
    public class GameOverConditionTestBase
    {
        /// <summary>
        /// Shared by the WinCondition Test Suites
        /// </summary>
        /// <param name="rows">The number of rows to initialise the board with</param>
        /// <param name="columns">The number of column to initialise the board with</param>
        /// <param name="expectedValidMoveCount">The number of times board.ValidMove is expected to be fired</param>
        /// <param name="expectedInvalidMoveCount">The number of times board.InvalidMove is expected to be fired</param>
        /// <param name="expectedGameOverCount">The number of times board.ValidMove is expected to be fired where isGameOver == true</param>
        /// <param name="columnMoves">An int array of columns to simulate player moves</param>
        /// <param name="expectedWinnerType">The expected Type of the winner (either YellowPiece, RedPiece or null for draw)</param>
        /// <param name="expectedGameOverType">The expected Type of the win.  HorizontalWinCondition, VerticalWinCondition, DiagonalWinCondition or DrawCondition</param>
        public static void RunGameOverConditionTest(int rows, int columns, int expectedValidMoveCount, int expectedInvalidMoveCount, int expectedGameOverCount, int[] columnMoves, Type expectedWinnerType, Type expectedGameOverType)
        {
            Dimensions dimension = new Dimensions { Rows = rows, Columns = columns };
            PlayingBoard board = new PlayingBoard(dimension);

            int validMoveCount = 0;
            int invalidMoveCount = 0;
            int gameWonCount = 0;
            Type gameOverType = null;

            board.ValidMove += delegate (bool isGameOver, Type gameOverTypeIn)
            {
                validMoveCount++;
                if (isGameOver)
                {
                    gameWonCount++;
                    gameOverType = gameOverTypeIn;
                }

            };
            board.InvalidMove += delegate { invalidMoveCount++; };

            foreach (int column in columnMoves)
            {
                board.DropPieceIntoColumn(column);
            }

            Assert.AreEqual(expectedValidMoveCount, validMoveCount, "Valid moves");
            Assert.AreEqual(expectedInvalidMoveCount, invalidMoveCount, "Invalid moves");
            Assert.AreEqual(expectedGameOverCount, gameWonCount, "Game over count");
            Assert.AreEqual(expectedWinnerType, (expectedGameOverType == typeof(DrawGameOverCondition)) ? null : board.TurnMonitor.CurrentPlayerPiece.GetType(), "Winner Type");
            Assert.AreEqual(expectedGameOverType, gameOverType, "Game Over Type");
        }

    }
}
