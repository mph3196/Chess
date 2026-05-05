using Chess.Enums;
namespace Chess;

public class SquareState
{
    private int _rank;
    private BoardFile _file;
    private bool _isSelected;
    private PieceInfo? _occupant;

    public SquareState(int rank, BoardFile file, bool selected, PieceInfo? occupant)
    {
        _rank = rank;
        _file = file;
        _isSelected = selected;
        _occupant = occupant;
    }

    public int Rank
    {
        get { return _rank; }
    }
    public BoardFile File
    {
        get { return _file; }
    }

    public bool Selected
    {
        get { return _isSelected; }
        set { _isSelected = value; }
    }

    public bool Occupied
    {
        get { return _occupant!=null; }
    }

    public PieceInfo? Occupant
    {
        get { return _occupant; }
    }
}