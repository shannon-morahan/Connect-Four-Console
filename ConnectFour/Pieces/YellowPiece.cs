namespace ConnectFour.Pieces
{
    /// <summary>
    /// Represents the character to print on the console if Yellow is in a position.
    /// </summary>
    public class YellowPiece : IPiece
    {
        /// <summary>
        /// The letter Y (when English).
        /// </summary>
        public string BoardRepresentationCharacter => Properties.Resources.YellowCharacter;

        /// <summary>
        /// The word "Yellow" (when English).
        /// </summary>
        public string ColorMessage => Properties.Resources.Yellow;

        /// <summary>
        /// The word "Yellows" (when English).
        /// </summary>
        public string ColorMessagePlural => Properties.Resources.Yellows;
    }
}
