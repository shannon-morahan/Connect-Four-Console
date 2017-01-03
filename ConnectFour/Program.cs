using System.Diagnostics.CodeAnalysis;
using ConnectFour.Core;

namespace ConnectFour
{
    /// <summary>
    /// Entry point class of the Connect Four Application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point for the console application - args are ignored.
        /// </summary>
        /// <param name="args">args are ignored.</param>
        [ExcludeFromCodeCoverage]
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            ConnectFourApplication application = new ConnectFourApplication();
            application.Start();
        }
    }
}
