using CopperDevs.Observable.Internal;

namespace CopperDevs.Observable;

public static class Observer
{
    private static readonly Dictionary<Type, List<IObserverAction>> Events = [];

    private static readonly Dictionary<Delegate, IObserverAction> Actions = [];

    public static void Add<T>(Action<T> action) where T : Event, new()
    {
        if (!Actions.ContainsKey(action))
            Actions.Add(action, new ObserverAction<T>(action));

        Add((ObserverAction<T>)Actions[action]);
    }

    public static void Add<T>(Action action) where T : Event, new()
    {
        if (!Actions.ContainsKey(action))
            Actions.Add(action, new ObserverAction<T>(action));

        Add((ObserverAction<T>)Actions[action]);
    }

    private static void Add<T>(ObserverAction<T> action) where T : Event, new()
    {
        if (!Events.ContainsKey(typeof(T)))
            Events.Add(typeof(T), []);

        if (!Events[typeof(T)].Contains(action))
            Events[typeof(T)].Add(action);
    }


    public static void Remove<T>(Action<T> action) where T : Event, new()
    {
        if (!Actions.ContainsKey(action))
            Actions.Add(action, new ObserverAction<T>(action));

        Remove((ObserverAction<T>)Actions[action]);
    }

    public static void Remove<T>(Action action) where T : Event, new()
    {
        if (!Actions.ContainsKey(action))
            Actions.Add(action, new ObserverAction<T>(action));

        Remove((ObserverAction<T>)Actions[action]);
    }

    private static void Remove<T>(ObserverAction<T> action) where T : Event, new()
    {
        if (!Events.ContainsKey(typeof(T)))
            Events.Add(typeof(T), []);

        if (Events[typeof(T)].Contains(action))
            Events[typeof(T)].Add(action);
    }

    public static void Notify<T>(bool parallel) where T : Event, new()
    {
        Notify<T>(null, parallel);
    }

    public static void Notify<T>(T? targetEvent = null, bool parallel = false) where T : Event, new()
    {
        if (!Events.ContainsKey(typeof(T)))
            Events.Add(typeof(T), []);

        if (parallel)
            Parallel.ForEach(Events[typeof(T)], action => action.Invoke(targetEvent));
        else
            Events[typeof(T)].ForEach(action => action.Invoke(targetEvent));
    }
}