using System;

namespace ConnectFour.Pieces
{
    /// <summary>
    /// Represents a "Blank" space on a Connect Four board.
    /// </summary>
    public class EmptyPiece : IPiece
    {
        /// <summary>
        /// How do we represent a blank piece on the console?
        /// </summary>
        public string BoardRepresentationCharacter => Properties.Resources.EmptyCharacter;

        /// <summary>
        /// Not implemented because blank pieces are any colour
        /// </summary>
        public string ColorMessage
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Not implemented because blank pieces are any colour
        /// </summary>
        public string ColorMessagePlural
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}
