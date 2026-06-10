
using Chess.Enums;

namespace Chess;

public class BoardState
{
    private List<SquareState> _squares;
    private PieceColor _currentTurn;
    private bool _isCheck;
    private bool _isCheckmate;
    private int _turnNumber;
    private string _difficulty;

    public BoardState()
    {
       _squares = new List<SquareState>();
       _currentTurn = PieceColor.WHITE;
       _isCheck = false;
       _isCheckmate = false;
       _turnNumber = 0;
    }

    public BoardState(List<SquareState> squares, PieceColor currentTurn, bool check, bool checkmate, int turnNumber)
    {
       _squares = squares;
       _currentTurn = currentTurn;
       _isCheck = check;
       _isCheckmate = checkmate;
       _turnNumber = turnNumber;
    }

    public void UpdateDifficulty(string difficulty)
    {
        _difficulty = difficulty;
    }

    public List<SquareState> Squares
    {
        get { return _squares; }
    }

    public PieceColor CurrentTurn
    {
        get { return _currentTurn; }
    }

    public bool IsCheck
    {
        get { return _isCheck; }
    }

    public bool IsCheckmate
    {
        get { return _isCheckmate; }
    }

    public int TurnNumber
    {
        get { return _turnNumber; }
    }

    public string Difficulty
    {
        get { return _difficulty; }
    }

}