using System.Text;
using Chess.Enums;
using Chess.Model.Pieces;

namespace Chess.Model
{
    public class FENEncoder
    {
        List<Square> _squares;
        public FENEncoder(List<Square> squares)
        {
            _squares = squares;
        }

        public string Encode()
        {
            StringBuilder fen = new StringBuilder();
            SquareLookup lookup = new SquareLookup(_squares);

            for (int rank = 8; rank >= 1; rank--)
            {
                int emptySquares = 0;
                for (BoardFile file = BoardFile.A; file <= BoardFile.H; file++)
                {
                    Square s = lookup.GetSquare(rank, file);
                    if (s != null && s.Occupied)
                    {
                        if (emptySquares > 0)
                        {
                            fen.Append(emptySquares);
                            emptySquares = 0;
                        }
                        fen.Append(PieceToFen(s.Occupant));
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
            return c;
        }
    }
}