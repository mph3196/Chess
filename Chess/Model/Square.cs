using Chess.Enums;
namespace Chess.Model;

public class Square
{
    private int _rank;
    private BoardFile _file;
    private Piece? _piece;

    public Square(int rank, BoardFile file)
    {
        _rank = rank;
        _file = file;
    }

    public BoardFile File
    {
        get { return _file; }
        set { _file = value; }
    }

    public int Rank
    {
        get { return _rank; }
        set { _rank = value; }
    }

    public Piece? Piece
    {
        get {return _piece; }
        set { _piece = value; }
    }
}