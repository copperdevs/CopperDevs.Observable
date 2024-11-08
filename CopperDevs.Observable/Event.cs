namespace CopperDevs.Observable;

public abstract class Event
{
    public virtual void Attached() { }
    public abstract void Notified();
    public virtual void Detached() { }
}
