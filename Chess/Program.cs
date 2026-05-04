using SplashKitSDK;
using Chess.Model;
using Chess.View;
using Chess.Controller;

namespace Chess;

public class Program
{
    public static void Main()
    {
        Window window = new Window("MAIchess", 480, 480);

        BoardModel model = new BoardModel();
        BoardView view = new BoardView(window);
        BoardController controller = new BoardController(model, view);

        while (!window.CloseRequested)
        {
            SplashKit.ProcessEvents();
            window.Clear(Color.Red);
            view.DrawBoard();          
            window.Refresh();  
        }
    }
}