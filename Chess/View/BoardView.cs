using Chess.Enums;
using Chess.Model;

namespace Chess.View;

public class BoardView
{
    private List<Square> squares;
    public event Action<int, BoardFile>? SquareClicked;
    public BoardView()
    {
        
    }

    public void DrawBoard(List<Square> squares)
    {
        
    }

}