using SplashKitSDK;

namespace Chess;

public class Program
{
    public static void Main()
    {
        Window window = new Window("MAIchess", 480, 480);

        while (!window.CloseRequested)
        {
            SplashKit.ProcessEvents();
        }
    }
}