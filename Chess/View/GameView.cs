using Chess.Enums;
using Chess.Interfaces;
using SplashKitSDK;

namespace Chess.View;

public class GameView : Subject<IScreenClickedObserver>
{
    private readonly Window _window;
    private readonly int _squareSize;
    private BoardState? _boardState;
    private readonly Panel _panel;
    private readonly Dictionary<string, Bitmap> _sprites;

    public GameView(Window window)
    {
        _window = window;
        _squareSize = window.Height / 8;
        _panel = new Panel(_window.Height, 0, _window.Width - _window.Height, _window.Height, Color.DarkKhaki);
        _sprites = LoadSprites();
    }

    public void Update()
    {
        _window.Clear(Color.Red);
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            Point2D pos = SplashKit.MousePosition();

            int file = (int)(pos.X / _squareSize);
            int rank = 7 - (int)(pos.Y / _squareSize) + 1; // + 1 to convert pixel to rank, 7- to invert clicks

            if (file >= 0 && file <= 7 && rank >= 1 && rank <= 8)
            {
                Console.WriteLine($"Notifying observers of click at: rank={rank}, file={file}");
                NotifyObservers(observer => observer.OnSquareClicked(rank, (BoardFile)file));
            }
            else
            {
                Console.WriteLine("Click was outside of the board");
            }
            foreach (Button button in _panel.Buttons)
            {
                if (button.IsAt(pos))
                {
                    NotifyObservers(observer => observer.OnButtonClicked(button.Action));
                }
            }
        }
        DrawBoard();
        _window.Refresh();
    }

    public void UpdateDisplay(BoardState boardState)
    {
        _boardState = boardState;
        DrawBoard();
    }

    public void DrawBoard()
    {
        Color squareColor;
        List<SquareState> squares = _boardState!.Squares;
        foreach (SquareState s in squares)
        {
            double x = ((int)s.File) * _squareSize;
            double y = (9 - s.Rank - 1) * _squareSize; // -1 to convert rank to pixel, 9- to invert board drawing

            squareColor = (s.Rank + (int)s.File) % 2 == 0 ? Color.Maroon : Color.AntiqueWhite;
            _window.FillRectangle(squareColor, x, y, _squareSize, _squareSize);
            if (s.Selected)
            {
                _window.FillRectangle(Color.Red, x + 5, y + 5, _squareSize -10 , _squareSize - 10);
                _window.FillRectangle(squareColor, x + 10, y + 10, _squareSize - 20, _squareSize - 20);
            }

            if (s.Occupied)
            {
                DrawPiece(s.Occupant!, x , y);
            }
        }
        _panel.Draw(_boardState.IsCheck, _boardState.IsCheckmate, _boardState.Difficulty, _boardState.WhitePlayer, _boardState.BlackPlayer, _boardState.CurrentPlayer);
    }

    private void DrawPiece(PieceInfo piece, double x, double y)
    {
        string color = piece.Color == PieceColor.WHITE ? "w" : "b";
        string type = piece.Type.ToString().ToLower();
        string key = $"{color}_{type}";
        Bitmap sprite = _sprites[key];

        double pieceSize = _squareSize;
        double scale = pieceSize / sprite.Width;

        double pieceX = x + (_squareSize - pieceSize) / 2;
        double pieceY = y + (_squareSize - pieceSize) / 2;

        double offsetX = pieceSize * 0.2;
        double offsetY = pieceSize * 0.2;
        pieceX -= offsetX;
        pieceY -= offsetY;

        SplashKit.DrawBitmap(sprite, pieceX, pieceY, SplashKit.OptionScaleBmp(scale, scale));

    }

    private Dictionary<string, Bitmap> LoadSprites()
    {
        Dictionary<string, Bitmap> sprites = new Dictionary<string, Bitmap>();
        for (PieceType p = PieceType.PAWN; p <= PieceType.KING; p++)
        {
            string key = $"w_{p.ToString().ToLower()}";
            sprites[$"{key}"] = SplashKit.LoadBitmap(key, $"Sprites\\{key}.png");
            key = $"b_{p.ToString().ToLower()}";
            sprites[$"{key}"] = SplashKit.LoadBitmap(key, $"Sprites\\{key}.png");
        }
        return sprites;
    }

        public Window Window
    {
        get { return _window ; }
    }

}