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
    }


}

