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
        GameView view = new GameView(window);
        GameModel model = new GameModel();
        GameController controller = new GameController(model, view);
        controller.Run();
    }
}