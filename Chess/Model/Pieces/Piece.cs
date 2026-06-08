using Chess.Enums;

namespace Chess.Model.Pieces;

public abstract class Piece
{
    protected PieceColor _color;
    protected PieceType _type;
    protected bool _hasMoved;

    protected Piece(PieceColor color, PieceType type)
    {
        _color = color;
        _type = type;
        _hasMoved = false;
    }

    public PieceColor Color
    {
        get { return _color; }
    }

    public PieceType Type
    {
        get { return _type; }
    }

    public bool HasMoved
    {
        get { return _hasMoved; }
        set { _hasMoved = value; }
    }

    public abstract List<Move> GetLegalMoves(int currentRank, BoardFile currentFile, List<Square> squares, SquareLookup lookup);


}