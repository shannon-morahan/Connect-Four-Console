using ConnectFour.Core;
using NUnit.Framework;

namespace ConnectFour.Tests.Core
{
    [TestFixture]
    public class DimensionTests
    {        
        /// <summary>
        /// Do we get a correct Dimensions object when we use the minimum bounds?
        /// </summary>
        [Test]
        public void DimensionCanParseMinimumSizeCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse($"{Dimensions.MinumumRows} {Dimensions.MinumumColumns}", out dimensions, out errorMessage);
            Assert.That(result);
            Assert.That(dimensions.Rows == Dimensions.MinumumRows);
            Assert.That(dimensions.Columns == Dimensions.MinumumColumns);
            Assert.That(errorMessage == null);
        }

        /// <summary>
        /// Do we get a correct Dimensions object when we use the maximum bounds?
        /// </summary>
        [Test]
        public void DimensionCanParseMaximumSizeCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse($"{Dimensions.MaximumRows} {Dimensions.MaximumColumns}", out dimensions, out errorMessage);
            Assert.That(result);
            Assert.That(dimensions.Rows == Dimensions.MaximumRows);
            Assert.That(dimensions.Columns == Dimensions.MaximumColumns);
            Assert.That(errorMessage == null);
        }

        /// <summary>
        /// Do we get a correct Dimensions object when we use bounds inside min and max?
        /// </summary>
        [Test]
        public void DimensionCanParseMixedSizeCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse($"{Dimensions.MinumumRows} {Dimensions.MaximumColumns}", out dimensions, out errorMessage);
            Assert.That(result);
            Assert.That(dimensions.Rows == Dimensions.MinumumRows);
            Assert.That(dimensions.Columns == Dimensions.MaximumColumns);
            Assert.That(errorMessage == null);
        }

        /// <summary>
        /// Do we get an error message if we pass in a value to low for rows?
        /// </summary>
        [Test]
        public void DimensionRowTooLowFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse($"{Dimensions.MinumumRows - 1} {Dimensions.MinumumColumns}", out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }

        /// <summary>
        /// Do we get an error message if we pass in a value to high for rows?
        /// </summary>
        [Test]
        public void DimensionRowTooHighFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse($"{Dimensions.MaximumRows + 1} {Dimensions.MinumumColumns}", out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }

        /// <summary>
        /// Do we get an error message if we pass in a value to low for columns?
        /// </summary>
        [Test]
        public void DimensionColumnTooLowFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse($"{Dimensions.MinumumRows} {Dimensions.MinumumColumns - 1}", out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }

        /// <summary>
        /// Do we get an error message if we pass in a value to high for columns?
        /// </summary>
        [Test]
        public void DimensionColumnTooHighFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse($"{Dimensions.MinumumRows} {Dimensions.MaximumColumns + 1}", out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }

        /// <summary>
        /// Do we get an error message if we pass a non-numeric value as a row?
        /// </summary>
        [Test]
        public void DimensionNonNumberAtFrontFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse("a 5", out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }

        /// <summary>
        /// Do we get an error message if we pass a non-numeric value as a column?
        /// </summary>
        [Test]
        public void DimensionNonNumberAtBackFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse("5 a", out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }

        /// <summary>
        /// Do we get an error message if we pass a non-numeric value as a row and a column?
        /// </summary>
        [Test]
        public void DimensionNonNumberInMiddleFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse("5 a 5", out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }

        /// <summary>
        /// Do we get an error message if we pass a null?
        /// </summary>
        [Test]
        public void DimensionNullWithNullInputFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse(null, out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }

        /// <summary>
        /// Do we get an error message if we pass a an empty string?
        /// </summary>
        [Test]
        public void DimensionNullWithEmptyInputFailsToParseCorrectly()
        {
            Dimensions dimensions;
            string errorMessage;
            bool result = Dimensions.TryParse("", out dimensions, out errorMessage);
            Assert.That(result == false);
            Assert.That(dimensions == null);
            Assert.That(Properties.Resources.InvalidDimensions == errorMessage);
        }
    }
}
