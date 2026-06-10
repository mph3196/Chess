
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
    private string _whitePlayer;
    private string _blackPlayer;

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

    public void UpdateStateFromController(string difficulty, string whitePlayer, string blackPlayer)
    {
        _difficulty = difficulty;
        _whitePlayer = whitePlayer;
        _blackPlayer = blackPlayer;
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

    public string WhitePlayer
    {
        get { return _whitePlayer; }
    }

    public string BlackPlayer
    {
        get { return _blackPlayer; }
    }

    public string CurrentPlayer
    {
        get { return _currentTurn.ToString(); }
    }

}