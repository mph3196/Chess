using Chess.Enums;
using Chess.Interfaces;
using Chess.Model.Pieces;

namespace Chess.Model.Factories;
public class QueenFactory : IPieceFactory
{
    public List<Piece> CreatePieces()
    {
        List<Piece> pieces = new List<Piece>();

        for (int i = 0; i < 4; i++)
        {
            pieces.Add(new Queen(PieceColor.WHITE));
        }
        pieces.Add(new King(PieceColor.WHITE));
        for (int i = 0; i < 11; i++)
        {
            pieces.Add(new Queen(PieceColor.WHITE));
        }

        for (int i = 0; i < 12; i++)
        {
            pieces.Add(new Queen(PieceColor.BLACK));
        }
        pieces.Add(new King(PieceColor.BLACK));
        for (int i = 0; i < 4; i++)
        {
            pieces.Add(new Queen(PieceColor.BLACK));
        }
        
        return pieces;
    }
}
