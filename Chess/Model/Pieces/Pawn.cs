using Chess.Enums;

namespace Chess.Model.Pieces;

public class Pawn : Piece
{
    public Pawn(PieceColor color) : base(color, PieceType.PAWN)
    {
        
    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares)
    {
        List<Move> legalMoves = new List<Move>();
        return legalMoves;
    }
    
}