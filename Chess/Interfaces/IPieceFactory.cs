using Chess.Model.Pieces;

namespace Chess.Interfaces;

public interface IPieceFactory
{
    List<Piece> CreatePieces();
}