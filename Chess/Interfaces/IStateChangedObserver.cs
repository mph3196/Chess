namespace Chess.Interfaces;

public interface IStateChangedObserver
{
    public void OnModelStateChanged();
}