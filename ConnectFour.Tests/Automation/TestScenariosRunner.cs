using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using NUnit.Framework;
using TestScenario = ConnectFour.Tests.Automation.ConsoleTestScenariosConsoleTestScenario;
using ExpectStandardOutToPrint = ConnectFour.Tests.Automation.ConsoleTestScenariosConsoleTestScenarioExpectStandardOutToPrint;
using WriteLineToStandardInput = ConnectFour.Tests.Automation.ConsoleTestScenariosConsoleTestScenarioWriteLineToStandardInput;

namespace ConnectFour.Tests.Automation
{
    /// <summary>
    /// Runs the data-driven tests specified in \\Automation\\ConsoleTestScenarios.xml
    /// </summary>
    [TestFixture]
    public class TestScenariosRunner
    {
        /// <summary>
        /// Reads the Scenarios from ConsoleTestScenarios.xml and then runs them as tests (Data Driven Tests)
        /// </summary>
        [Test, TestCaseSource(nameof(ReadConsoleTestScenariosList))]        
        public void ExecuteScenario(TestScenario scenario)
        {
            List<string> capturedOutput = new List<string>();
            List<string> expectedOutput = new List<string>();
            string standardInputExpectedOverhang = string.Empty;

            // Not pretty, but it gets the job done.  I know of no other way to get a different projects folder
            // Would need to be updated if changed to release version, CI builds etc.
            DirectoryInfo solutionDirectory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory).Parent?.Parent?.Parent;

            Debug.Assert(solutionDirectory != null, "solutionDirectory != null");
            FileInfo connectFourExecutable = new FileInfo(Path.Combine(solutionDirectory.FullName, @"ConnectFour\bin\Debug\ConnectFour.exe"));

            // Spawn a new process, redirecting StdIn and StdOut so we can listen
            Process currentProcess = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = connectFourExecutable.FullName,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true
                }
            };

            // Capture the output so we can validate it against the scenarios
            currentProcess.OutputDataReceived += (sender, args) =>
            {
                if(args?.Data != null)
                {
                    capturedOutput.Add(args.Data.Trim());
                }
            };

            currentProcess.Start();
            currentProcess.BeginOutputReadLine();

            // This is a little 'Domain Specific Language Parser'
            // The "Domain" is our terminal test language
            Debug.Assert(scenario?.Items != null, "scenario?.Items != null");

            foreach (object item in scenario.Items)
            {
                if(item is ExpectStandardOutToPrint)
                {
                    ExpectStandardOutToPrint expectation = item as ExpectStandardOutToPrint;
                    string trimmedExpectedOuput = expectation.Value.Trim();

                    if(expectation.StdInLine)
                    {
                        standardInputExpectedOverhang += expectation.Value;
                    }
                    else
                    {
                        expectedOutput.Add(standardInputExpectedOverhang + trimmedExpectedOuput);
                        standardInputExpectedOverhang = string.Empty;
                    }
                }
                else if(item is WriteLineToStandardInput)
                {
                    // Write something to standard In
                    WriteLineToStandardInput simulatedEntry = item as WriteLineToStandardInput;
                    string trimmedString = simulatedEntry.Value.Trim(); // Microsoft doesn't support xmlspace:Collapse :)
                    currentProcess.StandardInput.WriteLine(trimmedString);
                }
                else
                {
                    throw new InvalidDataException("One of the ConsoleTestScenarios contains an unknown element (Perhaps update Edit => Paste Special => Paste XML as classes?");
                }

            }

            bool hasExited = currentProcess.WaitForExit(2000);


            if(hasExited == false)
            {
                currentProcess.Kill();
                currentProcess.Dispose();
                Assert.Fail($"Process did not complete for test {scenario.Name}");
            }
            else
            {
                currentProcess.Dispose();
                Assert.AreEqual(expectedOutput.Count, capturedOutput.Count, $"Line count was not the same for test {scenario.Name}");
                for(int i=0; i<expectedOutput.Count; i++)
                {
                    Assert.AreEqual(expectedOutput[i], capturedOutput[i], $"Line {i} was not the same for test {scenario.Name}");
                }
            }
            
        }

        /// <summary>
        /// This method returns all the individual TestScenario objects read from TestScenarios.xml.
        /// 
        /// It is used in the above method to execute the tests based on the data (i.e. Data Driven Test).
        /// </summary>
        /// <returns>Unserialized TestScenarios in TestScenarios.xml</returns>
        private static IEnumerable<TestScenario> ReadConsoleTestScenariosList()
        {
            string codebase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string scenariosXmlFile = $"{codebase}\\Automation\\TestScenarios.xml".Replace("file:\\", ""); // executing folder

            using (TextReader reader = new StreamReader(scenariosXmlFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ConsoleTestScenarios));
                ConsoleTestScenarios scenarios = (ConsoleTestScenarios)serializer.Deserialize(reader);
                return scenarios.ConsoleTestScenario;
            }
        }
    }
}
