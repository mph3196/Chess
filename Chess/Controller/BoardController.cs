using Chess.Model;
using Chess.View;

namespace Chess.Controller;

public class BoardController
{
    BoardModel _model;
    BoardView _view;

    public BoardController(BoardModel model, BoardView view)
    {
        _model = model;
        _view = view;
    }
}

