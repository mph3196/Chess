
using Chess.Enums;

namespace Chess;

public class BoardState
{
    private List<SquareState> _squares;
    private PieceColor _currentTurn;
    private bool _isCheck;
    private bool _isCheckmate;
    private int _turnNumber;


}