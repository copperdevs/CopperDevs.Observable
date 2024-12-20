using CopperDevs.Core.Utility;

namespace CopperDevs.Observable.Internal;

internal class ObserverAction<TEvent> : IObserverAction where TEvent : Event, new()
{
    private readonly Action? baseAction;
    private readonly Action<TEvent>? valueAction;

    private Delegate ActiveAction => (baseAction is not null ? baseAction : valueAction)!;

    public ObserverAction(Action baseAction) => this.baseAction = baseAction;

    public ObserverAction(Action<TEvent> valueAction) => this.valueAction = valueAction;

    public void Invoke(object? data)
    {
        baseAction?.Invoke();
        
        if ((Observer.ActionsNullableValues[ActiveAction]) || data != null || !typeof(TEvent).HasAnyValues())
            valueAction?.Invoke((TEvent)data!);
    }
}