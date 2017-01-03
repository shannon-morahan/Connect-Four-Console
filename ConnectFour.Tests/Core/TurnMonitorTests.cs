using ConnectFour.Core;
using ConnectFour.Pieces;
using NUnit.Framework;

namespace ConnectFour.Tests.Core
{
    [TestFixture]
    public class TurnMonitorTests
    {
        /// <summary>
        /// Does TurnMonitor start with Yellow?
        /// </summary>
        [Test]
        public void TurnMonitorStartsWithYellow()
        {
            TurnMonitor monitor = new TurnMonitor();
            Assert.That(monitor.CurrentPlayerPiece is YellowPiece);
        }

        /// <summary>
        /// Does TurnMonitor start with Yellow and then swap to Red?
        /// </summary>
        [Test]
        public void TurnMonitorSwapsFromYellowToRed()
        {
            TurnMonitor monitor = new TurnMonitor();
            monitor.SwapPiece();
            Assert.That(monitor.CurrentPlayerPiece is RedPiece);
        }

        /// <summary>
        /// Does TurnMonitor start with Yellow, Swap to Red then Swap to Yellow?
        /// </summary>
        [Test]
        public void TurnMonitorSwapsFromRedToYellow()
        {
            TurnMonitor monitor = new TurnMonitor();
            monitor.SwapPiece();
            Assert.That(monitor.CurrentPlayerPiece is RedPiece);
            monitor.SwapPiece();
            Assert.That(monitor.CurrentPlayerPiece is YellowPiece);
        }

        /// <summary>
        /// Does TurnMonitor start with Yellow, Swap to Red, Swap to Yellow, Swap to Red and then swap to Yellow?
        /// </summary>
        [Test]
        public void TurnMonitorSwapsFromYellowToRedToYellowToRedToYellow()
        {
            TurnMonitor monitor = new TurnMonitor();
            monitor.SwapPiece();
            Assert.That(monitor.CurrentPlayerPiece is RedPiece);
            monitor.SwapPiece();
            Assert.That(monitor.CurrentPlayerPiece is YellowPiece);
            monitor.SwapPiece();
            Assert.That(monitor.CurrentPlayerPiece is RedPiece);
            monitor.SwapPiece();
            Assert.That(monitor.CurrentPlayerPiece is YellowPiece);
        }
    }
}
