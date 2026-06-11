using System.Text;
using Chess.Enums;
using Chess.Model.Pieces;

namespace Chess.Model
{
    public class FENEncoder
    {
        public FENEncoder()
        {
            
        }

        public string Encode(char sideToMove, string castling, string enPassant, int halfMoveClock, int fullMoveCounter, List<Square> squares)
        {
            StringBuilder fen = new StringBuilder();
            SquareLookup lookup = new SquareLookup(squares);

            for (int rank = 8; rank >= 1; rank--)
            {
                int emptySquares = 0;
                for (BoardFile file = BoardFile.A; file <= BoardFile.H; file++)
                {
                    Square s = lookup.GetSquare(rank, file);
                    if (s.Occupied)
                    {
                        if (emptySquares > 0)
                        {
                            fen.Append(emptySquares);
                            emptySquares = 0;
                        }
                        fen.Append(PieceToFen(s.Occupant!));
                    }
                    else
                    {
                        emptySquares++;
                    }
                }
                if (emptySquares > 0)
                {
                    fen.Append(emptySquares);
                }
                if (rank > 1)
                {
                    fen.Append('/');
                }
            }

            fen.Append(" ");
            fen.Append(sideToMove);
            fen.Append(" ");
            fen.Append(castling);
            fen.Append(" ");
            fen.Append(enPassant);
            fen.Append(" ");
            fen.Append(halfMoveClock);
            fen.Append(" ");
            fen.Append(fullMoveCounter);

            Console.WriteLine($"FEN encoded: {fen.ToString()}");
            return fen.ToString();
        }

        private char PieceToFen(Piece piece)
        {
            char c = piece.Type switch
            {
                PieceType.PAWN => 'p',
                PieceType.KNIGHT => 'n',
                PieceType.BISHOP => 'b',
                PieceType.ROOK => 'r',
                PieceType.QUEEN => 'q',
                PieceType.KING => 'k',
                _ => '?'
            };
            c = piece.Color == PieceColor.WHITE ? char.ToUpper(c) : c;
            return c;
        }
    }
}