using Chess.Enums;
namespace Chess.Model;

public class Square
{
    int _rank;
    BoardFile _file;

    public Square(int rank, BoardFile file)
    {
        _rank = rank;
        _file = file;
    }

    public BoardFile File
    {
        get { return _file; }
    }
}