using Chess.Enums;

namespace Chess.Interfaces;

public interface ISquareClickedObserver
{
    void OnSquareClicked(int rank, BoardFile file);
}