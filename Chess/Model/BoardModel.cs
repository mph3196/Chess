using Chess.Enums;

namespace Chess.Model;

public class BoardModel
{
    List<Square> _squares;
    public event Action? StateChanged;

    public BoardModel()
    {
        _squares = new List<Square>();
    }

    public void SquareClicked(int rank, BoardFile file)
    {
        
    }
}