using SplashKitSDK;

namespace Chess.View;

public class Panel
{
    private double _x;
    private double _y;
    private double _width;
    private double _height;
    private Color _color;
    private List<Button> _buttons;

    public Panel(double x, double y, double width, double height, Color color)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
        _color = color;
        _buttons = new List<Button>();
        MakeButtons();
    }

    public List<Button> Buttons
    {
        get { return _buttons; }
    }

    private void MakeButtons()
    {
        double x = _x + 20;
        double width = _width * 0.66;
        Button newGame = new Button(x, 20, width, "New Game", "newGame");
        _buttons.Add(newGame);
        Button whitePlayer = new Button(x, 120, width, "Swap White", "swapWhite");
        _buttons.Add(whitePlayer);
        Button blackPlayer = new Button(x, 220, width, "Swap Black", "swapBlack");
        _buttons.Add(blackPlayer);
        Button lowerDifficulty = new Button(x, 320, width, "Lower Difficulty", "lowerDifficulty");
        _buttons.Add(lowerDifficulty);
        Button increaseDifficulty = new Button(x, 420, width, "Increase Difficulty", "increaseDifficulty");
        _buttons.Add(increaseDifficulty);
    }

    public void Draw(bool isCheck, bool isCheckmate, string difficulty)
    {
        SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        foreach (Button button in _buttons)
        {
            button.Draw();
        }
        
        SplashKit.DrawText($"Difficulty: {difficulty}", Color.Black, _x + 10, _y + 500);
        if (isCheckmate)
        {
            SplashKit.DrawText($"CHECKMATE!", Color.SwinburneRed, _x + 10, _y + 600);
        }
        else if (isCheck)
        {
            SplashKit.DrawText("CHECK", Color.SwinburneRed, _x + 10, _y + 600);
        }
    }
}