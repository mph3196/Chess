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
        SquareLookup lookup = new SquareLookup(squares);

        foreach (Square s in squares)
        {
            int rankDiff = Math.Abs(s.Rank - rank);
            int fileDiff = Math.Abs((int)s.File - (int)file);

            bool sameRank = s.Rank == rank && s.File != file;
            bool sameFile = s.File == file && s.Rank != rank;
            bool sameDiagonal = rankDiff == fileDiff && rankDiff > 0;

            if (!sameRank && !sameFile && !sameDiagonal)
            {
                continue;
            }

            int rankStep = 0;
            int fileStep = 0;
            if (sameRank)
            {
                fileStep = (int)s.File > (int)file ? 1 : -1;
            }
            else if (sameFile)
            {
                rankStep = s.Rank > rank ? 1: -1;
            }
            else
            {
                rankStep = s.Rank > rank ? 1 : -1;
                fileStep = (int)s.File > (int)file ? 1 : -1;
            }

            int r = rank + rankStep;
            int f = (int)file + fileStep;
            bool blocked = false;

            while (r != s.Rank || f != (int)s.File)
            {
                if (lookup.GetSquare(r, (BoardFile)f)?.Occupied == true)
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