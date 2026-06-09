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

                if (_model.CurrentTurn == PieceColor.WHITE && !testRequest)
                {
                    Console.WriteLine("FETCHING REQUEST");
                    string fen = _model.ToFEN();
                    string response = _stockfish.SendRequestSync(fen);
                    if (response != null)
                    {
                        Console.WriteLine(response);
                        testRequest = true;
                    }
                    else
                    {
                        Console.WriteLine("Stockfish unavailable");
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



}

