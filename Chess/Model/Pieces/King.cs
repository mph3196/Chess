using Chess.Enums;

namespace Chess.Model.Pieces;

public class King : Piece
{
    public King(PieceColor color) : base(color, PieceType.KING)
    {

    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares)
    {
        List<Move> legalMoves = new List<Move>();
        return legalMoves;
    }
    
}