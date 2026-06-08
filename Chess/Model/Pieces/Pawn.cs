using Chess.Enums;

namespace Chess.Model.Pieces;

public class Pawn : Piece
{
    private bool _enPassant;
    public Pawn(PieceColor color) : base(color, PieceType.PAWN)
    {
        _enPassant = false;
    }

    public override List<Move> GetLegalMoves(int rank, BoardFile file, List<Square> squares, SquareLookup lookup)
    {
        int direction = this.Color == PieceColor.WHITE ? 1 : -1;
        List<Move> legalMoves = new List<Move>();
        
        Square oneAhead = lookup.GetSquare(rank + direction, file);
        if (oneAhead != null && !oneAhead.Occupied)
        {
            legalMoves.Add(new Move(rank, file, oneAhead.Rank, oneAhead.File));

            if (!HasMoved)
            {
                Square twoAhead = lookup.GetSquare(rank + 2 * direction, file);
                if (twoAhead != null && !twoAhead.Occupied)
                {
                    legalMoves.Add(new Move(rank, file, twoAhead.Rank, twoAhead.File));
                }
            }
        }

        int[] offsets = { -1, 1 };
        foreach (int offset in offsets)
        {
            int target = (int)file + offset;
            if (target < 1 || target > 8)
            {
                continue;
            }

            Square diagonal = lookup.GetSquare(rank + direction, (BoardFile)target);
            if (diagonal == null)
            {
                continue;
            }

            if (diagonal.Occupied && diagonal.Occupant.Color != this.Color)
            {
                legalMoves.Add(new Move(rank, file, diagonal.Rank, diagonal.File));
            }
        }

        return legalMoves;
    }
    
}