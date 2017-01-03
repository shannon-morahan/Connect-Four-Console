using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConnectFour.Core;
using NUnit.Framework;

namespace ConnectFour.Tests.Core
{
    [TestFixture]
    public class ConnectFourApplicationTests
    {
        /// <summary>
        /// Does ConnectFourApplication.RequestDimensions() correctly read "5 5" as dimensions?
        /// </summary>
        [Test]
        public void ConnectionFourApplicationRequestDimensionsWorksForCorrectInput()
        {
            CapturingConsoleTextWriter capturing = new CapturingConsoleTextWriter();
            Console.SetOut(capturing);

            Console.SetIn(new StandardInSimulator(new List<string> { "5 5" }));

            Dimensions dimensions = ConnectFourApplication.RequestDimensions();
            Assert.AreEqual(5, dimensions.Rows);
            Assert.AreEqual(5, dimensions.Columns);
            Assert.AreEqual(2, capturing.LastLines.Count);
            Assert.AreEqual(Properties.Resources.AskForDimensions, capturing.LastLines[0]);
            Assert.AreEqual(Properties.Resources.Prompt, capturing.LastLines[1]);
        }

        /// <summary>
        /// Does ConnectFourApplication.RequestDimensions() show an error when we type "error" for the dimensions?
        /// </summary>
        [Test]
        public void ConnectionFourApplicationRequestDimensionsWorksForIncorrectInput()
        {
            CapturingConsoleTextWriter capturing = new CapturingConsoleTextWriter();
            Console.SetOut(capturing);

            Console.SetIn(new StandardInSimulator(new List<string> { "error", "5 5" }));

            Dimensions dimensions = ConnectFourApplication.RequestDimensions();
            Assert.AreEqual(5, dimensions.Rows);
            Assert.AreEqual(5, dimensions.Columns);
            Assert.AreEqual(5, capturing.LastLines.Count);
            Assert.AreEqual(Properties.Resources.AskForDimensions, capturing.LastLines[0]);
            Assert.AreEqual(Properties.Resources.Prompt, capturing.LastLines[1]);
            Assert.AreEqual(Properties.Resources.InvalidDimensions, capturing.LastLines[2]);
            Assert.AreEqual(Properties.Resources.AskForDimensions, capturing.LastLines[3]);
            Assert.AreEqual(Properties.Resources.Prompt, capturing.LastLines[4]);
        }

        /// <summary>
        /// Does ConnectFourApplication.RequestDimensions() show an error when we enter "" for the dimensions?
        /// </summary>
        [Test]
        public void ConnectionFourApplicationRequestDimensionsWorksForEmptyInput()
        {
            CapturingConsoleTextWriter capturing = new CapturingConsoleTextWriter();
            Console.SetOut(capturing);

            Console.SetIn(new StandardInSimulator(new List<string> { "", "5 5" }));

            Dimensions dimensions = ConnectFourApplication.RequestDimensions();
            Assert.AreEqual(5, dimensions.Rows);
            Assert.AreEqual(5, dimensions.Columns);
            Assert.AreEqual(5, capturing.LastLines.Count);
            Assert.AreEqual(Properties.Resources.AskForDimensions, capturing.LastLines[0]);
            Assert.AreEqual(Properties.Resources.Prompt, capturing.LastLines[1]);
            Assert.AreEqual(Properties.Resources.InvalidDimensions, capturing.LastLines[2]);
            Assert.AreEqual(Properties.Resources.AskForDimensions, capturing.LastLines[3]);
            Assert.AreEqual(Properties.Resources.Prompt, capturing.LastLines[4]);
        }

        /// <summary>
        /// Does ConnectFourApplication.GetValidIntegerFromUser correctly parse the string "1"?
        /// </summary>
        [Test]
        public void ConnectionFourApplicationGetValidIntegerFromUserWorksForCorrectInput()
        {
            PlayingBoard board = new PlayingBoard(new Dimensions { Rows = 5, Columns = 5 });
            PlayingBoardView view = new PlayingBoardView(board);

            Console.SetIn(new StandardInSimulator(new List<string> { "1" }));

            int userColumnConvertedTo0IndexColumn = ConnectFourApplication.GetValidIntegerFromUser(board, view);
            Assert.AreEqual(0, userColumnConvertedTo0IndexColumn);
        }

        /// <summary>
        /// Does ConnectFourApplication.GetValidIntegerFromUser show an error message for the input "a"
        /// </summary>
        [Test]
        public void ConnectionFourApplicationGetValidIntegerFromUserWorksForInorrectInput()
        {
            PlayingBoard board = new PlayingBoard(new Dimensions { Rows = 5, Columns = 5 });
            PlayingBoardView view = new PlayingBoardView(board);

            Console.SetIn(new StandardInSimulator(new List<string> { "a", "1" }));

            CapturingConsoleTextWriter capturing = new CapturingConsoleTextWriter();
            Console.SetOut(capturing);

            ConnectFourApplication.GetValidIntegerFromUser(board, view);

            Assert.AreEqual(7, capturing.LastLines.Count);
            Assert.AreEqual(Properties.Resources.UnableToUnderstandAsANumber, capturing.LastLines[3]);
        }

        /// <summary>
        /// Can we get a "Yellow WINS!" message by executing the simplest case?
        /// 
        /// Note this is a quick check.  The thourough tests of flow are in the automation suite.
        /// </summary>
        [Test]
        public void ConnectFourApplicationFlowWorksCorrectly()
        {
            ConnectFourApplication application = new ConnectFourApplication();

            Console.SetIn(new StandardInSimulator(new List<string> { "5 5", "1", "2", "1", "2", "1", "2", "1" }));


            CapturingConsoleTextWriter capturing = new CapturingConsoleTextWriter();
            Console.SetOut(capturing);

            application.Start();

            // This is just a basic test - the complete application flow is tested in the automation suite
            Assert.AreEqual(25, capturing.LastLines.Count);
            Assert.AreEqual($@"{Properties.Resources.Yellow} WINS!", capturing.LastLines[24]);            
        }

        #region Helpers
            /// <summary>
            /// Helper class to simulate writing to standard in
            /// </summary>
        private class StandardInSimulator : TextReader
        {
            private readonly Queue<string> ToSimulate;

            public StandardInSimulator(List<string> lines)
            {
                ToSimulate = new Queue<string>(lines);
            }

            public override string ReadLine()
            {
                return ToSimulate.Count > 0 ? ToSimulate.Dequeue().Trim() : "";
            }
        }

        /// <summary>
        /// Helper class to read the lines from standard out
        /// </summary>
        private class CapturingConsoleTextWriter : TextWriter
        {
            public List<string> LastLines { get; } = new List<string>();

            public override Encoding Encoding => Encoding.UTF8;

            public override void Write(string value)
            {
                LastLines.Add(value);
                base.Write(value);
            }

            public override void WriteLine(string value)
            {
                string[] seperator = new string[] {Environment.NewLine};
                string[] strings = value.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in strings)
                {
                    LastLines.Add(str);
                }
                base.WriteLine(value);
            }
        }
        #endregion
    }
}
