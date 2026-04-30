using System.Runtime.CompilerServices;
using Chess.Enums;
using Chess.Interfaces;
using Chess.Model;
using Chess.View;

namespace Chess.Controller;

public class BoardController : ISquareClickedObserver, IStateChangedObserver
{
    BoardModel _model;
    BoardView _view;

    public BoardController(BoardModel model, BoardView view)
    {
        _model = model;
        _view = view;

        // Subscribe to observe model and view subjects
        _model.Subscribe(this);
        _view.Subscribe(this);
    }

    public void OnModelStateChanged()
    {
        Console.WriteLine("Controller: Model changed");
        _view.DrawBoard(_model.Squares);
    }

    public void OnSquareClicked(int rank, BoardFile file)
    {
        Console.WriteLine($"Controller: Received click at {file}{rank}");
    }



}

