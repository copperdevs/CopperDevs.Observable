namespace CopperDevs.Observer;

public abstract class Event
{
    public virtual void Attached() { }
    public abstract void Notified();
    public virtual void Detached() { }
}
