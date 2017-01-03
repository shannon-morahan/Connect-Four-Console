namespace ConnectFour.Pieces
{
    /// <summary>
    /// How do we represent a RedPiece
    /// </summary>
    public class RedPiece : IPiece
    {
        /// <summary>
        /// The letter R (when English).
        /// </summary>
        public string BoardRepresentationCharacter => Properties.Resources.RedCharacter;

        /// <summary>
        /// The word Red (when English).
        /// </summary>
        public string ColorMessage => Properties.Resources.Red;

        /// <summary>
        /// The word Reds (when English).
        /// </summary>
        public string ColorMessagePlural => Properties.Resources.Reds;
    }
}
