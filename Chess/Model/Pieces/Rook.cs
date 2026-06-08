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

        foreach (Square s in squares)
        {
            bool sameRank = s.Rank == rank && s.File != file;
            bool sameFile = s.File == file && s.Rank != rank;

            if (!sameRank && !sameFile)
            {
                continue;
            }

            int rankStep = 0;
            int fileStep = 0;
            if (sameRank)
            {
                fileStep = (int)s.File > (int)file ? 1: -1;
            }
            else
            {
                rankStep = s.Rank > rank ? 1 : -1;
            }

            int r = rank + rankStep;
            int f = (int)file + fileStep;
            bool blocked = false;

            while (r != s.Rank || f != (int)s.File)
            {
                if (lookup.GetSquare(r, (BoardFile)f).Occupied == true)
                {
                    blocked = true;
                    break;
                }
                r += rankStep;
                f += fileStep;
            }

            if (!blocked)
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