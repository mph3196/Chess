using Chess.Enums;

namespace Chess.Interfaces;

public interface ISquareClickedObserver
{
    public void OnSquareClicked(int rank, BoardFile file);
}