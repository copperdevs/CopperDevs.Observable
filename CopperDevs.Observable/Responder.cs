using CopperDevs.Core.Utility;

namespace CopperDevs.Observable;

public abstract class Responder<T> : SafeDisposable where T : Event, new()
{
    protected Responder()
    {
    }

    public override void DisposeResources()
    {
        Observer.Remove<T>(Notified);
    }

    protected abstract void Notified(T? data);

    private void Initialize(bool nullableValue = false)
    {
        Observer.Add<T>(Notified, nullableValue);
    }

    public static TResponder Create<TResponder>(bool nullableValue = false)
        where TResponder : Responder<T>, new()
    {
        var responder = new TResponder();
        responder.Initialize(nullableValue);
        return responder;
    }
}