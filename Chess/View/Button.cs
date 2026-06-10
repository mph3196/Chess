using SplashKitSDK;

namespace Chess.View;

public class Button
{
    private double _x;
    private double _y;
    private double _width;
    private double _height;
    private Color _color;
    private Color _fontColor;
    private string _label;
    private string _action;

    public Button(double x, double y, double width, string label, string action)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = width * 0.2;
        _color = Color.Olive;
        _fontColor = Color.White;
        _label = label;
        _action = action;

    }

    public string Action
    {
        get { return _action; }
    }

    public void Draw()
    {
        SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        SplashKit.DrawText(_label, _fontColor, _x + 10, _y + 10);
    }

    public bool IsAt(Point2D pt)
    {
        return SplashKit.PointInRectangle(pt.X, pt.Y, _x, _y, _width, _height);
    }

}