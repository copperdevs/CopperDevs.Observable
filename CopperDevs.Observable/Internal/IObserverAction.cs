namespace CopperDevs.Observable.Internal;

internal interface IObserverAction
{
    public void Invoke(object? data);
}