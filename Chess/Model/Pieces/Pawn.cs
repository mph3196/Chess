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
        foreach (Square s in squares)
        {
            if (s.Rank == rank + (1 * direction) && s.File == file)
            {
                if (!s.Occupied)
                {
                    Move move1 = new Move(rank, file, rank + (1 * direction), file);
                    legalMoves.Add(move1);
                    if (!HasMoved && s.Rank == rank + (2 * direction))
                    {
                        Move move2 = new Move(rank, file, rank + (2 * direction), file);
                        legalMoves.Add(move2);
                    }
                }
            }
            if (!HasMoved && s.Rank == rank + (2 * direction))
            {
                Move move2 = new Move(rank, file, rank + (2 * direction), file);
                legalMoves.Add(move2);
            }
        }
        return legalMoves;
    }
    
}