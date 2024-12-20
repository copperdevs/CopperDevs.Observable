using CopperDevs.Core.Utility;

namespace CopperDevs.Observable;

public abstract class Responder<T> : SafeDisposable where T : Event, new()
{
    public Responder()
    {
        Observer.Add<T>(Notified);
    }

    public override void DisposeResources()
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