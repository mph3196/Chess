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

        _model.StateChanged += OnModelStateChanged;
        _view.SquareClicked += OnSquareClicked;
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

