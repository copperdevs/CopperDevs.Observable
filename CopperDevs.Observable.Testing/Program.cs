using CopperDevs.Logger;

namespace CopperDevs.Observable.Testing;

public static class Program
{
    public static void Main()
    {
        Observer.Add<PlayerDamageEvent>(OnDamage, true);
        Observer.Add<PlayerDamageEvent>(OnDamaged, true);
        Observer.Add<EmptyEventTest>(EmptyMoment);

        Observer.Notify<PlayerDamageEvent>();

        Log.Debug("*imagine a bit of time later*");

        Observer.Notify(new PlayerDamageEvent { Amount = 10 });

        Log.Debug("*imagine even more bits of time later*");
        
        Observer.Notify<EmptyEventTest>();
        Observer.Notify<EmptyEventTest>();
        Observer.Notify<EmptyEventTest>();

        Observer.Remove<EmptyEventTest>(EmptyMoment);
        Observer.Remove<PlayerDamageEvent>(OnDamage);
        Observer.Remove<PlayerDamageEvent>(OnDamaged);
    }

    public static void OnDamaged()
    {
        Log.Info("Player has been damaged");
    }

    public static void OnDamage(PlayerDamageEvent? e)
    {
        Log.Info($"Damage Amount: {e?.Amount ?? 0}");
    }

    public static void EmptyMoment()
    {
        Log.Info("empty moment :3");
    }
}

public sealed record PlayerDamageEvent : Event
{
    public int Amount;
}

public sealed record EmptyEventTest : Event;