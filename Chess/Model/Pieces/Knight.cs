using Chess.Enums;

namespace Chess.Model.Pieces;

public class Knight : Piece
{
    public Knight(PieceColor color) : base(color, PieceType.KNIGHT)
    {
        
    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares)
    {
        List<Move> legalMoves = new List<Move>();
        SquareLookup lookup = new SquareLookup(squares);

        foreach (Square s in squares)
        {
            int rankDiff = Math.Abs(s.Rank - rank);
            int fileDiff = Math.Abs((int)s.File - (int)file);

            if ((rankDiff == 2 && fileDiff == 1) || (rankDiff == 1 && fileDiff == 2))
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