using SplashKitSDK;

namespace Chess.View;

public class Button
{
    double _x;
    double _y;
    double _width;
    double _height;
    Color _color;
    Color _fontColor;
    string _label;
    string _action;

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