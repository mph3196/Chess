using Chess.Enums;

namespace Chess.Model.Pieces;

public abstract class Piece
{
    private PieceColor _color;
    private PieceType _type;
    private bool _hasMoved;

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
    }

    public abstract List<Move> GetLegalMoves(int currentRank, BoardFile currentFile, List<Square> boardState);


}