using Chess.Enums;

namespace Chess.Model.Pieces;

public class Bishop : Piece
{
    public Bishop(PieceColor color) : base(color, PieceType.BISHOP)
    {
        
    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares, SquareLookup lookup)
    {
        List<Move> legalMoves = new List<Move>();

        foreach (Square s in squares)
        {
            int rankDiff = Math.Abs(s.Rank - rank);
            int fileDiff = Math.Abs((int)s.File - (int)file);
            if (rankDiff == fileDiff && rankDiff > 0)
            {
                int rankStep = s.Rank > rank ? 1 : -1;
                int fileStep = (int)s.File > (int)file ? 1 : -1;

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

        }

        return legalMoves;
    }
    
}