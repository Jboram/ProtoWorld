public abstract class SingletonBase<T> where T : SingletonBase<T>, new()
{
    public static T inst
    {
        get;
        private set;
    }

    protected virtual void OnClear() { }

    protected virtual void OnCreated()
    {

    }

    static SingletonBase()
    {
        if (inst == null)
        {
            inst = new T();
            inst.OnCreated();
        }
    }

    public void Clear()
    {
        OnClear();
        inst = null;
    }
}
