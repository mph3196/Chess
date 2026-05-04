using Chess.Enums;

namespace Chess.Model.Pieces;

public class Queen : Piece
{
    public Queen(PieceColor color) : base(color, PieceType.QUEEN)
    {

    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares)
    {
        List<Move> legalMoves = new List<Move>();
        return legalMoves;
    }
    
}