using SplashKitSDK;

using Chess.Enums;
using Chess.Interfaces;
using Chess.Model;
using Chess.View;

namespace Chess.Controller;

public class BoardController : ISquareClickedObserver, IStateChangedObserver
{
    BoardModel _model;
    BoardView _view;
    BoardState _boardState;
    StockfishHTTP _stockfish;
    private Player _whitePlayer = Player.HUMAN;
    private Player _blackPlayer = Player.AI;

    bool testRequest;

    public BoardController(BoardModel model, BoardView view)
    {
        _model = model;
        _view = view;
        _boardState = new BoardState();
        _stockfish = new StockfishHTTP();

        _model.Subscribe(this);
        _view.Subscribe(this);
        testRequest = false;
    }

    public void Run()
    {
        OnModelStateChanged();
        
        while (!_view.Window.CloseRequested)
        {
            try
            {
                SplashKit.ProcessEvents();
                _view.Window.Clear(Color.Red);
                _view.Update();
                _view.DrawBoard();          
                _view.Window.Refresh();

                Player currentPlayer = _model.CurrentTurn == PieceColor.WHITE  ? _whitePlayer : _blackPlayer;
                if (currentPlayer == Player.AI)
                {
                    string fen = _model.ToFEN();
                    string uci = _stockfish.GetMoveFromResponse(fen);
                    if (uci != null)
                    {
                        Move move = DecodeUCI(uci);
                        Console.WriteLine("UCI decoded");
                        _model.ExecuteMove(move);
                        Console.WriteLine("AI move executed");
                        _model.AdvanceTurn();
                        OnModelStateChanged();
                        Console.WriteLine($"AI moved: {uci}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
        }
    }

    public void OnModelStateChanged()
    {
        Console.WriteLine("Controller: Model changed");
        var boardState = _model.GetBoardState();
        _view.UpdateDisplay(boardState);
    }


    public void OnSquareClicked(int rank, BoardFile file)
    {
        Console.WriteLine($"Controller: Received click at {file}{rank}");
        _model.SquareClicked(rank, file);
    }

    private Move DecodeUCI(string uci)
    {
        char fromFileChar = uci[0];
        char fromRankChar = uci[1];
        char toFileChar   = uci[2];
        char toRankChar   = uci[3];

        BoardFile fromFile = (BoardFile)(char.ToLower(fromFileChar) - 'a');
        int      fromRank  = fromRankChar - '0';
        BoardFile toFile   = (BoardFile)(char.ToLower(toFileChar) - 'a');
        int      toRank    = toRankChar - '0';

        return new Move(fromRank, fromFile, toRank, toFile);
    }



}

