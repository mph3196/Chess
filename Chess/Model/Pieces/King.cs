using Chess.Enums;

namespace Chess.Model.Pieces;

public class King : Piece
{
    public King(PieceColor color) : base(color, PieceType.KING)
    {

    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares, SquareLookup lookup)
    {
        List<Move> legalMoves = new List<Move>();

        foreach (Square s in squares)
        {
            int rankDiff = Math.Abs(s.Rank - rank);
            int fileDiff = Math.Abs((int)s.File - (int)file);

            if (rankDiff <= 1 && fileDiff <= 1 && (rankDiff !=0 || fileDiff != 0))
            {
                if (!s.Occupied || (s.Occupied && s.Occupant.Color != this.Color))
                {
                    legalMoves.Add(new Move(rank, file, s.Rank, s.File));
                }
            }
        }
        
        return legalMoves;
    }
    
}