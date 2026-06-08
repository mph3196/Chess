using Chess.Enums;
using Chess.Interfaces;
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
        _squareSize = window.Height/8;
    }

    public void Update()
    {
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            Point2D pos = SplashKit.MousePosition();

            int file = (int)(pos.X / _squareSize);
            Console.WriteLine(file);
            int rank = (int)(pos.Y / _squareSize) + 1; // + 1 to convert pixel to rank
            Console.WriteLine(rank);

            if (file >= 0 && file <= 7 && rank >= 1 && rank <= 8)
            {
                Console.WriteLine($"Notifying observers: rank={rank}, file={file}");
                NotifyObservers(observer => observer.OnSquareClicked(rank, (BoardFile)file));
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
        Color squareColor;
        PieceInfo piece;
        List<SquareState> squares = _boardState.Squares;
        foreach (SquareState s in squares)
        {
            double x = ((int)s.File) * _squareSize;
            double y = (s.Rank - 1) * _squareSize; // -1 to convert rank to pixel

            squareColor = (s.Rank + (int)s.File) % 2 == 0 ? Color.Maroon : Color.AntiqueWhite;
            _window.FillRectangle(squareColor, x, y, _squareSize, _squareSize);
            if (s.Selected)
            {
                _window.FillRectangle(Color.Red, x + 5, y + 5, _squareSize -10 , _squareSize - 10);
                _window.FillRectangle(squareColor, x + 10, y + 10, _squareSize - 20, _squareSize - 20);
            }

            if (s.Occupied)
            {
                DrawPiece(s.Occupant, x , y);
            }
        }
    }

    private void DrawPiece(PieceInfo piece, double x, double y)
    {
        double pieceX = x + (_squareSize / 2);
        double pieceY = y + (_squareSize / 2);
        Color color = piece.Color == PieceColor.WHITE ? Color.White : Color.Black;
        switch (piece.Type)
        {
            case PieceType.PAWN:
                _window.FillCircle(color, pieceX, pieceY, _squareSize / 3);
                break;
            default:
                _window.FillRectangle(color, pieceX, pieceY, _squareSize / 3, _squareSize / 3);
                break;
        }
    }

    public Window Window
    {
        get { return _window ; }
    }

}