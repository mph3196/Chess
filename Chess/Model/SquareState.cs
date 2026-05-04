using Chess.Enums;
namespace Chess;

public class SquareState
{
    private int _rank;
    private BoardFile _file;
    private bool isSelected;
    private PieceInfo? Piece;
}