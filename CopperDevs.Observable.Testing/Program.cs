using CopperDevs.Logger;

namespace CopperDevs.Observable.Testing;

public static class Program
{
    public static void Main()
    {
        Observer.Add<PlayerDamageEvent>(TestThing);
        Observer.Notify<PlayerDamageEvent>();
        Observer.Remove<PlayerDamageEvent>(TestThing);
    }

    public static void TestThing()
    {
        Log.Info("Test Thing");
    }

    public sealed record PlayerDamageEvent : Event
    {
        public int Test;
    }
}