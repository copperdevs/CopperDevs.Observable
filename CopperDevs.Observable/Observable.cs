namespace CopperDevs.Observer;

public abstract class Observable
{
    public virtual void Attached() { }
    public abstract void Notified();
    public virtual void Detached() { }
}
