
using Chess.Enums;

namespace Chess;

public class BoardState
{
    private readonly List<SquareState> _squares;
    private readonly string _currentPlayer;
    private readonly bool _isCheck;
    private readonly bool _isCheckmate;
    private readonly int _turnNumber;
    private string _difficulty;
    private string _whitePlayer;
    private string _blackPlayer;

    public BoardState(List<SquareState> squares, PieceColor currentPlayer, bool check, bool checkmate, int turnNumber)
    {
       _squares = squares;
       _currentPlayer = currentPlayer.ToString();
       _isCheck = check;
       _isCheckmate = checkmate;
       _turnNumber = turnNumber;
       _difficulty = "";
       _whitePlayer = "";
       _blackPlayer = "";
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
        get { return _currentPlayer; }
    }

}