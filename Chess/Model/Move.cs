using Chess.Enums;

namespace Chess.Model;

public class Move
{
    private int _fromRank;
    private BoardFile _fromFile;
    private int _toRank;
    private BoardFile _toFile;

    public Move(int fromRank, BoardFile fromFile, int toRank, BoardFile toFile)
    {
        _fromRank = fromRank;
        _fromFile = fromFile;
        _toRank = toRank;
        _toFile = toFile;
    }

    
}