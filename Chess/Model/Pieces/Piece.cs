using Chess.Enums;

namespace Chess.Model.Pieces;

public abstract class Piece
{
    private PieceColor _color;
    private PieceType _type;

    protected Piece(PieceColor color, PieceType type)
    {
        _color = color;
        _type = type;
    }

    public PieceColor Color
    {
        get { return _color; }
    }

    public PieceType Type
    {
        get { return _type; }
    }

}