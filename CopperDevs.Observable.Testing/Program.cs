using CopperDevs.Logger;

namespace CopperDevs.Observable.Testing;

public static class Program
{
    public static void Main()
    {
        Observer.Add<PlayerDamageEvent>(OnDamage);
        Observer.Add<PlayerDamageEvent>(OnDamaged);

        Observer.Notify<PlayerDamageEvent>();
        Log.Debug("*imagine a bit of time later*");
        Observer.Notify(new PlayerDamageEvent { Amount = 10 });

        Observer.Remove<PlayerDamageEvent>(OnDamage);
        Observer.Remove<PlayerDamageEvent>(OnDamaged);
    }

    public static void OnDamaged()
    {
        Log.Info("Player has been damaged");
    }

    public static void OnDamage(PlayerDamageEvent e)
    {
        Log.Info($"Damage Amount: {e.Amount}");
    }

    public sealed record PlayerDamageEvent : Event
    {
        public int Amount;
    }
}