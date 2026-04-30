using Chess.Enums;
using Chess.Interfaces;
using Chess.Model;
using SplashKitSDK;

namespace Chess.View;

public class BoardView : Subject<ISquareClickedObserver>
{
    private Window _window;
    private const int SQUARE_SIZE = 60;

    private List<Square> squares;
    public event Action<int, BoardFile>? SquareClicked;
    public BoardView(Window window)
    {
        _window = window;
    }

    public void DrawBoard(List<Square> squares)
    {
        Color color;
        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                double x = rank * SQUARE_SIZE;
                double y = file * SQUARE_SIZE;

                if (rank + file % 2 == 0)
                {
                    color = Color.Maroon;
                } else
                {
                    color = Color.AntiqueWhite;
                }

                _window.FillRectangle(color, x, y, SQUARE_SIZE, SQUARE_SIZE);
            }
        }
    }

}