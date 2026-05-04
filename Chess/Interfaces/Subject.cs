namespace Chess.Interfaces;

public abstract class Subject<T> where T : class
{
    private List<T> _observers = new List<T>();
    public void Subscribe(T observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            Console.WriteLine($"Subject: {observer} subscribed.");
        }
    }

    public void Unsubscribe(T observer)
    {
        _observers.Remove(observer);
        Console.WriteLine($"Subject: {observer} unsubscribed.");
    }

    protected void NotifyObservers(Action<T> notification)
    {
        Console.WriteLine($"Subject: Notifying {_observers.Count} observers");
        foreach (var observer in _observers)
        {
            notification(observer);
        }
    }
}