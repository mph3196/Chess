using Chess.Enums;
using Chess.Model.Pieces;
namespace Chess.Model;

public class Square
{
    private int _rank;
    private BoardFile _file;
    private Piece? _piece;
    private bool _isSelected;

    public Square(int rank, BoardFile file)
    {
        _rank = rank;
        _file = file;
    }

    public bool AreYou(int rank, BoardFile file)
    {
        bool areYou = false;
        if (Rank == rank  && File == file)
        {
            areYou = true;
        }
        return areYou;
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

    public Piece? Occupant
    {
        get { return _piece; }
        set { _piece = value; }
    }

    public bool Selected
    {
        get { return _isSelected; }
        set { _isSelected = value; }
    }

    public bool Occupied
    {
        get { return _piece != null; }
    }
}