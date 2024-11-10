namespace CopperDevs.Observable;

public abstract class Responder<T> : IDisposable where T : Event, new()
{
    private bool hasDisposed = false;

    public Responder()
    {
        Observer.Add<T>(Notified);
    }

    ~Responder()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool manual)
    {
        if (hasDisposed)
            return;

        hasDisposed = true;
        DisposeResources();
    }

    private void DisposeResources()
    {
        Observer.Remove<T>(Notified);
    }

    protected abstract void Notified(T data);

    public static TResponder Create<TResponder>()
        where TResponder : Responder<T>, new()
    {
        return new TResponder();
    }
}