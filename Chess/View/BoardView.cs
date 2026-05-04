using Chess.Enums;
using Chess.Interfaces;
using Chess.Model;
using SplashKitSDK;

namespace Chess.View;

public class BoardView : Subject<ISquareClickedObserver>
{
    private Window _window;
    private int _squareSize;

    private BoardState _boardState;
    public BoardView(Window window)
    {
        _window = window;
        _squareSize = 64;
    }

    public void UpdateDisplay(BoardState boardState)
    {
        _boardState = boardState;
        DrawBoard();
    }

    public void DrawBoard()
    {
        Color color;
        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                double y = rank * _squareSize;
                double x = file * _squareSize;

                if ((rank + file) % 2 == 0)
                {
                    color = Color.Maroon;
                } else
                {
                    color = Color.AntiqueWhite;
                }

                _window.FillRectangle(color, x, y, _squareSize, _squareSize);
            }
        }
    }

}