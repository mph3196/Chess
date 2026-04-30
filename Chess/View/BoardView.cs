using Chess.Enums;
using Chess.Model;

namespace Chess.View;

public class BoardView
{
    public List<Square> squares;
    public event Action<int, BoardFile>? SquareClicked;
    public BoardView()
    {
        
    }

    public void DrawBoard(List<Square> squares)
    {
        
    }

}