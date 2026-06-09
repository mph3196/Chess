using Chess.Enums;
namespace Chess.Model;

public class SquareLookup
{
    private Dictionary<(int Rank, BoardFile file), Square> _map;

    public SquareLookup(List<Square> squares)
    {
        _map = new Dictionary<(int, BoardFile), Square>();
        foreach (Square s in squares)
        {
            _map[(s.Rank, s.File)] = s;
        }
    }

    public Square GetSquare(int rank, BoardFile file)
    {
        _map.TryGetValue((rank, file), out Square s);
        return s;
    }

    
}