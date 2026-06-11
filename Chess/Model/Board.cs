using Chess.Enums;
using Chess.Model.Pieces;

namespace Chess.Model;

public class Board
{
    private readonly List<Square> _squares;
    private readonly SquareLookup _lookup;

    public Board()
    {
        _squares = new List<Square>(64);
        for (int rank = 1; rank <= 8; rank++)
            for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
                _squares.Add(new Square(rank, f));
        _lookup = new SquareLookup(_squares);
    }

    public void PlacePieces(List<Piece> pieces)
    {
        int i = 0;
        // White back rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
            GetSquare(1, f).Occupant = pieces[i++];
        // White front rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
            GetSquare(2, f).Occupant = pieces[i++];
        // Black front rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
            GetSquare(7, f).Occupant = pieces[i++];
        // Black back rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
            GetSquare(8, f).Occupant = pieces[i++];
    }

    public Square GetSquare(int rank, BoardFile file)
    {
        return _lookup.GetSquare(rank, file);
    }
    public List<Square> Squares
    {
        get { return _squares; }
    }
}