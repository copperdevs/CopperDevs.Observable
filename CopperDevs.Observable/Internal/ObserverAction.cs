namespace CopperDevs.Observable.Internal;

internal class ObserverAction<TEvent> : IObserverAction where TEvent : Event, new()
{
    private readonly Action? baseAction;
    private readonly Action<TEvent>? valueAction;

    public ObserverAction(Action baseAction) => this.baseAction = baseAction;

    public ObserverAction(Action<TEvent> valueAction) => this.valueAction = valueAction;

    public void Invoke(object? data)
    {
        baseAction?.Invoke();

        data ??= new TEvent();
        valueAction?.Invoke((TEvent)data);
    }
}
