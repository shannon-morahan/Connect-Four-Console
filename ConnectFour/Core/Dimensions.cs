namespace ConnectFour.Core
{
    /// <summary>
    /// Represents the rows and columns in a Connect Four game.
    /// 
    /// Also includes a string parser to convert from a string to a Dimensions Object
    /// </summary>
    public class Dimensions
    {
        /// <summary>
        /// The least amount of rows allowed.  Inclusive.
        /// </summary>
        public static readonly int MinumumRows = 5;

        /// <summary>
        /// The least amount of columns allowed.  Inclusive.
        /// </summary>
        public static readonly int MinumumColumns = 5;

        /// <summary>
        /// The maximum number of rows allowed.  Inclusive.
        /// </summary>
        public static readonly int MaximumRows = 200;

        /// <summary>
        /// The maximum number of columns allowed.  Inclusive.
        /// </summary>
        public static readonly int MaximumColumns = 200;

        /// <summary>
        /// What seperator are we using to split the two numbers?
        /// </summary>
        public static readonly char Delimiter = ' ';

        /// <summary>
        /// Contains the number of rows (top-to-bottom) in a Connect Four board.
        /// </summary>
        public int Rows;

        /// <summary>
        /// Contains the number of columns (left-to-right) in a Connect Four board.
        /// </summary>
        public int Columns;

        /// <summary>
        /// Trys to parse a string with two numbers representing (rows and columns).
        /// </summary>
        /// <param name="userEnteredLine">The input string given by the user</param>
        /// <param name="dimensions">If successful, the Dimensions object containing the dimensions given in the string</param>
        /// <param name="errormessage">If unsuccessful, the error message of why it couldn't be parsed</param>
        /// <returns>True if successful, false otherwise</returns>
        public static bool TryParse(string userEnteredLine, out Dimensions dimensions, out string errormessage)
        {
            // We need something to parse
            if (string.IsNullOrEmpty(userEnteredLine))
            {
                errormessage = Properties.Resources.InvalidDimensions;
                dimensions = null;
                return false;
            }

            // Try to parse the given trimmed string correctly
            string trimmedString = userEnteredLine.Trim();

            string[] splitStrings = trimmedString.Split(Delimiter);

            // Need it to be exactly two items
            if (splitStrings.Length != 2)
            {
                errormessage = Properties.Resources.InvalidDimensions;
                dimensions = null;
                return false;
            }

            // Need both to parse as numbers
            int rows;
            int columns;
            if(!int.TryParse(splitStrings[0], out rows) || !int.TryParse(splitStrings[1], out columns))
            {
                errormessage = Properties.Resources.InvalidDimensions;
                dimensions = null;
                return false;
            }

            // Need both to meet the mimumums
            if(rows < MinumumRows || columns < MinumumColumns)
            {
                errormessage = Properties.Resources.InvalidDimensions;
                dimensions = null;
                return false;
            }

            // Need both to meet the minimums
            if (rows > MaximumRows || columns > MaximumColumns)
            {
                errormessage = Properties.Resources.InvalidDimensions;
                dimensions = null;
                return false;
            }

            // Should have two correct dimensions now - let's return it
            errormessage = null;
            dimensions = new Dimensions { Rows = rows, Columns = columns };
            return true;
        }

    }
}