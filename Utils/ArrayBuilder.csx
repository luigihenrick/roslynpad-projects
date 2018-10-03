public class ArrayBuilder<T>
{
    private List<List<string>> _total { get; set; }
    private List<string> _current { get; set; }
    private T _instance { get; set; }

    public ArrayBuilder(T instance)
    {
        _total = new List<List<string>>();
        _current = new List<string>();
        _instance = instance;
    }

    public ArrayBuilder<T> New()
    {
        _current = new List<string>();
        _total.Add(_current);

        return this;
    }

    public ArrayBuilder<T> AddLabelValue(string title, string value)
    {
        _current.Add(title);
        _current.Add(value);

        return this;
    }
    
    public ArrayBuilder<T> AddLabelValue(string title, Func<T, object> value)
    {
        _current.Add(title);
        _current.Add(value.Invoke(_instance)?.ToString());

        return this;
    }
    
    public ArrayBuilder<T> AddLabelValue(string title, string format, Func<T, object> value)
    {
        _current.Add(title);
        _current.Add(String.Format(format, value.Invoke(_instance)));

        return this;
    }

    public string[][] Build()
    {
        return _total.Select(s => s.ToArray()).ToArray();
    }
}