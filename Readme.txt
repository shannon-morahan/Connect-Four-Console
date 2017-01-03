Introduction

	Connect Four is a console based C# application developed using Behavior-Driven Development and Test-Driven Development principles. No application code was written before the Tests were written.

	It consists of two projects:

	- ConnectFour; The application source code.
	- ConnectFour.Test; Application tests; There are 55 of them.

	It also includes a small Domain Specific Language and parser I created to automate the testing of Connect Four from outside of the application code.

Project Dependencies & Tools

	- NUnit
	- dotCover was used to measure coverage and discover untested statements

Coverage

	Although coverage alone isn’t an indication of good testing: 

	-	dotCover reports coverage at 99% for the main project
	-	dotCover reports coverage at 95% for the test project

Levels of testing

	- Automation: To test the end-to-end flow of the system as a whole, a small automation suite was created.  It runs through an XML file (TestScenarios.xml) reading 9 scenarios, including the 6 acceptance criteria that were specified in the brief.  It works by feeding standard input into a spawned process and then checking the output and flow.
	- Sociable Tests: These are tests that use 1 or more "real" classes to form a “Unit” for testing.
	- Solitary Tests: These test the functionality of a single class.
	- Some explanation of these levels is available from Martin Fowler at http://martinfowler.com/bliki/UnitTest.html

Running the application

	-	The console application runs without any command line arguments, so just run it in debug mode for Visual Studio Users or just double click the ConnectFour.exe file.

Notes

	- The application is internationalised.