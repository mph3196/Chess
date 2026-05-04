using Chess.Enums;
using Chess.Interfaces;

namespace Chess.Model;

public class BoardModel : Subject<IStateChangedObserver>
{
    List<Square> _squares;

    public BoardModel()
    {
        _squares = new List<Square>();
    }

    public List<Square> Squares
    {
        get { return _squares; }
    }

    public void SquareClicked(int rank, BoardFile file)
    {
        
    }

    
}