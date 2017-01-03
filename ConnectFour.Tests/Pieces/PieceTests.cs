using System;
using ConnectFour.Pieces;
using NUnit.Framework;

namespace ConnectFour.Tests.Pieces
{
    [TestFixture]
    public class PieceTests
    {
        /// <summary>
        /// Do our strings match what we expect for i18n?
        /// </summary>
        [Test]
        public void PiecesReportStringsCorrectly()
        {
            Assert.AreEqual(new EmptyPiece().BoardRepresentationCharacter, Properties.Resources.EmptyCharacter);
            // See below for EmptyCharacter.ColorMessage and EmptyCharacter.ColorMessagePlural tests

            Assert.AreEqual(new YellowPiece().BoardRepresentationCharacter, Properties.Resources.YellowCharacter);
            Assert.AreEqual(new YellowPiece().ColorMessage, Properties.Resources.Yellow);
            Assert.AreEqual(new YellowPiece().ColorMessagePlural, Properties.Resources.Yellows);

            Assert.AreEqual(new RedPiece().BoardRepresentationCharacter, Properties.Resources.RedCharacter);
            Assert.AreEqual(new RedPiece().ColorMessage, Properties.Resources.Red);
            Assert.AreEqual(new RedPiece().ColorMessagePlural, Properties.Resources.Reds);
        }

        /// <summary>
        /// Does trying to use EmptyPiece.ColorMessage throw a NotSupportedException?
        /// </summary>
        [Test]
        public void PieceEmptyDoesNotSupportColorMessage()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                // ReSharper disable once UnusedVariable
                string neverAssigned = new EmptyPiece().ColorMessage;
            });
        }

        /// <summary>
        /// Does trying to use EmptyPiece.ColorMessagePlural throw a NotSupportedException?
        /// </summary>
        [Test]
        public void PieceEmptyDoesNotSupportColorMessagePlural()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                // ReSharper disable once UnusedVariable
                string neverAssigned = new EmptyPiece().ColorMessagePlural;
            });
        }
    }
}
