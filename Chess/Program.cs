using SplashKitSDK;
using Chess.Model;
using Chess.View;
using Chess.Controller;

namespace Chess;

public class Program
{
    public static void Main()
    {
        Window window = new Window("MAIchess", 1024, 768);

        BoardModel model = new BoardModel();
        BoardView view = new BoardView(window);
        BoardController controller = new BoardController(model, view);
        controller.OnModelStateChanged();

        while (!window.CloseRequested)
        {
            SplashKit.ProcessEvents();
            window.Clear(Color.Red);
            view.Update();
            view.DrawBoard();          
            window.Refresh();  
        }
    }
}