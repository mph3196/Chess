using Chess.Enums;

namespace Chess.Model.Pieces;

public class Knight : Piece
{
    public Knight(PieceColor color) : base(color, PieceType.KNIGHT)
    {
        
    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares, SquareLookup lookup)
    {
        List<Move> legalMoves = new List<Move>();
        return legalMoves;
    }
    
}