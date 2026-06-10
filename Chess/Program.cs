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
        BoardView view = new BoardView(window);
        BoardModel model = new BoardModel();
        BoardController controller = new BoardController(model, view);
        controller.Run();
    }
}