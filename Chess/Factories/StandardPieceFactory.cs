using Chess.Enums;
using Chess.Interfaces;
using Chess.Model.Pieces;

namespace Chess.Model.Factories;
public class StandardPieceFactory : IPieceFactory
{
    public List<Piece> CreatePieces()
    {
        List<Piece> pieces = new List<Piece>(32);
        
        // White back rank
        pieces.Add(new Rook(PieceColor.WHITE));
        pieces.Add(new Knight(PieceColor.WHITE));
        pieces.Add(new Bishop(PieceColor.WHITE));
        pieces.Add(new Queen(PieceColor.WHITE));
        pieces.Add(new King(PieceColor.WHITE));
        pieces.Add(new Bishop(PieceColor.WHITE));
        pieces.Add(new Knight(PieceColor.WHITE));
        pieces.Add(new Rook(PieceColor.WHITE));

        // White pawns
        for (int i = 0; i < 8; i++)
        {
            pieces.Add(new Pawn(PieceColor.WHITE));
        }

        // Black pawns
        for (int i = 0; i < 8; i++)
        {
            pieces.Add(new Pawn(PieceColor.BLACK));
        }

        // Black back rank
        pieces.Add(new Rook(PieceColor.BLACK));
        pieces.Add(new Knight(PieceColor.BLACK));
        pieces.Add(new Bishop(PieceColor.BLACK));
        pieces.Add(new Queen(PieceColor.BLACK));
        pieces.Add(new King(PieceColor.BLACK));
        pieces.Add(new Bishop(PieceColor.BLACK));
        pieces.Add(new Knight(PieceColor.BLACK));
        pieces.Add(new Rook(PieceColor.BLACK));

        return pieces;
    }
}
