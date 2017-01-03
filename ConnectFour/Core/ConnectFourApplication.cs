using ConnectFour.GameOverConditions;
using System;

namespace ConnectFour.Core
{
    /// <summary>
    /// The class that starts everything off with the Start() method.
    /// </summary>
    public class ConnectFourApplication
    {
        private PlayingBoard CurrentPlayingBoard;
        private PlayingBoardView CurrentPlayingBoardView;

        /// <summary>
        /// Starts the console application. Will remain in this method
        /// until the game is over.
        /// </summary>
        public void Start()
        {
            Dimensions dimensions = RequestDimensions();
            CurrentPlayingBoard = new PlayingBoard(dimensions);
            CurrentPlayingBoardView = new PlayingBoardView(CurrentPlayingBoard);
            
            // "Listen" for invalid moves so we can inform the console user
            CurrentPlayingBoard.InvalidMove += delegate (string errorMessage)
            {
                Console.WriteLine(errorMessage);
                CurrentPlayingBoard.DropPieceIntoColumn(GetValidIntegerFromUser(CurrentPlayingBoard, CurrentPlayingBoardView));
            };

            // "Listen" for valid moves so we can update the board and requst next input
            CurrentPlayingBoard.ValidMove += delegate (bool isGameOver, Type gameOverType)
            {
                if (isGameOver)
                {
                    Console.Write(CurrentPlayingBoardView.RenderAsString());

                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (gameOverType == typeof(DrawGameOverCondition))
                    {
                        Console.WriteLine(Properties.Resources.Draw);
                    }
                    else
                    {
                        Console.WriteLine($@"{CurrentPlayingBoard.TurnMonitor.CurrentPlayerPiece.ColorMessage} {Properties.Resources.WINS}");
                    }

                    // ReSharper disable once RedundantJumpStatement
                    // Originally used System.Environment exit here, but nunit didn't like it
                    // We're just returning back to the Main() method instead
                    return;
                }
                else
                {
                    CurrentPlayingBoard.DropPieceIntoColumn(GetValidIntegerFromUser(CurrentPlayingBoard, CurrentPlayingBoardView));
                }
            };

            CurrentPlayingBoard.DropPieceIntoColumn(GetValidIntegerFromUser(CurrentPlayingBoard, CurrentPlayingBoardView));
        }
        
        /// <summary>
        /// Requests the dimensions from the user until a valid value is recieved.
        /// 
        /// Valid dimensions are 5-200.
        /// 
        /// The valid format is say "5 5".
        /// 
        /// </summary>
        /// <returns>A Dimensions object with Rows equal to the first number and Columns equal to the second number</returns>
        public static Dimensions RequestDimensions()
        {
            Dimensions dimensions = null;

            while (dimensions == null)
            {
                Console.WriteLine(Properties.Resources.AskForDimensions);
                Console.Write(Properties.Resources.Prompt);

                string userEnteredLine = Console.ReadLine();
                string errorMessage;
                if (!Dimensions.TryParse(userEnteredLine, out dimensions, out errorMessage))
                {
                    Console.WriteLine(errorMessage);
                }
            }

            return dimensions;
        }

        /// <summary>
        /// Requests a valid integer from the user
        /// </summary>
        /// <param name="board">The PlayingBoard in use</param>
        /// <param name="view">The PlayingBoardView in use</param>
        /// <returns></returns>
        public static int GetValidIntegerFromUser(PlayingBoard board, PlayingBoardView view)
        {
            bool isValid = true;
            string userEnteredLine;
            int column;
            do
            {
                if (isValid == false)
                {
                    Console.WriteLine(Properties.Resources.UnableToUnderstandAsANumber);
                }
                Console.Write(view.RenderAsString());
                Console.WriteLine($@"{Properties.Resources.Prompt}{board.TurnMonitor.CurrentPlayerPiece.ColorMessagePlural} {Properties.Resources.Turn}:");
                Console.Write(Properties.Resources.Prompt);
                userEnteredLine = Console.ReadLine();

            } while (!(isValid = int.TryParse(userEnteredLine, out column)));

            return column - 1;
            // -1 because user columns start from 1, but we use a 0 index array            
        }
    }
}