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
        _squareSize = 32;
    }

    public void Update()
    {
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            Point2D pos = SplashKit.MousePosition();

            int file = (int)(pos.X / _squareSize);
            Console.WriteLine(file);
            int rank = (int)(pos.Y / _squareSize) + 1;
            Console.WriteLine(rank);

            if (file >= 0 && file < 8 && rank >=0 && rank <= 8)
            {
                BoardFile boardFile = (BoardFile)file;
                Console.WriteLine($"Notifying observers: rank={rank}, file={file}");
                NotifyObservers(observer => observer.OnSquareClicked(rank, boardFile));
            }
            else
            {
                Console.WriteLine("Click was outside of the board");
            }
        }
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