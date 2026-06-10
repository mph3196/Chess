using Chess.Enums;

namespace Chess.Interfaces;

public interface IScreenClickedObserver
{
    public void OnSquareClicked(int rank, BoardFile file);
    public void OnButtonClicked(string action);
}