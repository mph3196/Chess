using Chess.Enums;
namespace Chess;

public class PieceInfo
{
    private PieceColor _color;
    private PieceType _type;

    public PieceInfo(PieceColor color, PieceType type)
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