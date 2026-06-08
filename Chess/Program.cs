using SplashKitSDK;
using Chess.Model;
using Chess.View;
using Chess.Controller;
using Chess.Model.Factories;

namespace Chess;

public class Program
{
    public static void Main()
    {
        Window window = new Window("MAIchess", 1024, 768);

        BoardModel model = new BoardModel();
        BoardView view = new BoardView(window);
        BoardController controller = new BoardController(model, view);
        model.Initialise(new StandardPieceFactory());
        controller.Run();
    }
}