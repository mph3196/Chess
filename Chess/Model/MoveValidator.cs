using Chess.Enums;
using Chess.Model.Pieces;

namespace Chess.Model;

public class MoveValidator
{
    private Board _board;

    public MoveValidator(Board board)
    {
        _board = board;
    }

    private bool IsSquareAttacked(int rank, BoardFile file, PieceColor attackingColor)
    {
        bool squareAttacked = false;
        foreach (Square s in _board.Squares)
        {
            if (!s.Occupied || s.Occupant?.Color != attackingColor)
            {
                continue;
            }

            List<Move> moves = s.Occupant.GetLegalMoves(s.Rank, s.File, _board.Squares);
            foreach (Move move in moves)
            {
                if (move.ToRank == rank && move.ToFile == file)
                {
                    squareAttacked = true;
                }
            }
        }

        return squareAttacked;
    }

    public bool IsKingInCheck(PieceColor color)
    {
        bool kingChecked = false;
        foreach (Square s in _board.Squares)
        {
            if (s.Occupied && s.Occupant!.Type == PieceType.KING && s.Occupant.Color == color)
            {
                PieceColor opponent = color == PieceColor.WHITE ? PieceColor.BLACK : PieceColor.WHITE;
                kingChecked = IsSquareAttacked(s.Rank, s.File, opponent) ;
            }
        }
        return kingChecked;
    }

    private bool MoveLeavesKingChecked(Move move, PieceColor movingColor)
    {
        Square from = _board.GetSquare(move.FromRank, move.FromFile);
        Square to = _board.GetSquare(move.ToRank, move.ToFile);
        Piece? captured = to.Occupant;
        Piece movedPiece = from.Occupant!;

        to.Occupant = movedPiece;
        from.Occupant = null;

        bool kingInCheck = IsKingInCheck(movingColor);

        from.Occupant = movedPiece;
        to.Occupant = captured;

        return kingInCheck;
    }

    public List<Move> RemoveIllegalMoves(List<Move> moves)
    {
        List<Move> legalMoves = new List<Move>();

        foreach (Move m in moves)
        {
            Square s = _board.GetSquare(m.FromRank, m.FromFile);
            PieceColor occupantColor = s.Occupant!.Color;
            if (!MoveLeavesKingChecked(m, occupantColor))
            {
                legalMoves.Add(m);
            }
        }
        return legalMoves;
    }

}