namespace CopperDevs.Observable;

public static class Observer
{
    private static readonly Dictionary<Type, List<Event>> events = [];

    public static void Add<T>() where T : Event, new()
    {

    }

    public static void Remove<T>() where T : Event, new()
    {

    }


    public static void Notify<T>() where T : Event, new()
    {

    }
}
