using CopperDevs.Core.Utility;
using CopperDevs.Observable.Internal;

namespace CopperDevs.Observable;

public static class Observer
{
    private static readonly Dictionary<Type, List<IObserverAction>> Events = [];

    // every action has a connected observer action, even if it's registered to be invoked on its action
    private static readonly Dictionary<Delegate, IObserverAction> Actions = [];

    // Actions dictionary could be a tuple but thats no fun so this is the next best thing
    internal static readonly Dictionary<Delegate, bool> ActionsNullableValues = [];


    #region Add

    public static void Add<T>(Action<T> action, bool nullableValues = false) where T : Event, new()
    {
        if (!Actions.ContainsKey(action))
            Actions.Add(action, new ObserverAction<T>(action));

        ActionsNullableValues[action] = nullableValues;

        Add((ObserverAction<T>)Actions[action]);
    }

    public static void Add<T>(Action action, bool nullableValues = false) where T : Event, new()
    {
        if (!Actions.ContainsKey(action))
            Actions.Add(action, new ObserverAction<T>(action));

        ActionsNullableValues[action] = nullableValues;

        Add((ObserverAction<T>)Actions[action]);
    }

    #endregion

    #region Remove

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

    #endregion

    #region Observer Action

    private static void Add<T>(ObserverAction<T> action) where T : Event, new()
    {
        if (!Events.ContainsKey(typeof(T)))
            Events.Add(typeof(T), []);

        if (!Events[typeof(T)].Contains(action))
            Events[typeof(T)].Add(action);
    }

    private static void Remove<T>(ObserverAction<T> action) where T : Event, new()
    {
        if (!Events.ContainsKey(typeof(T)))
            Events.Add(typeof(T), []);

        if (Events[typeof(T)].Contains(action))
            Events[typeof(T)].Add(action);
    }

    #endregion


    #region Notify

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

    #endregion
}