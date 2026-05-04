using Chess.Enums;

namespace Chess.Model.Pieces;

public class Bishop : Piece
{
    public Bishop(PieceColor color) : base(color, PieceType.BISHOP)
    {
        
    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares)
    {
        List<Move> legalMoves = new List<Move>();
        return legalMoves;
    }
    
}