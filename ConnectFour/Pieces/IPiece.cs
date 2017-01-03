namespace ConnectFour.Pieces
{
    public interface IPiece
    {
        string BoardRepresentationCharacter { get; }
        string ColorMessage { get; }
        string ColorMessagePlural { get; }
    }
}