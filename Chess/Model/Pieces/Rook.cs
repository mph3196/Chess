using Chess.Enums;

namespace Chess.Model.Pieces;

public class Rook : Piece
{
    public Rook(PieceColor color) : base(color, PieceType.ROOK)
    {

    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares, SquareLookup lookup)
    {
        List<Move> legalMoves = new List<Move>();
        return legalMoves;
    }
    
}