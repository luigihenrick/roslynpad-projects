#r "$NuGet\Newtonsoft.Json\11.0.2\lib\net45\Newtonsoft.Json.dll"

using Newtonsoft.Json;

var asset = new Asset()
{
    AssetId = 20,
    AssetName = "COE 02"
};

var arrayBuilder = new ArrayBuilder<Asset>(asset);

var arrayMuitoLouco = arrayBuilder
    .New().AddLabelValue("Aplicação minima", "R$ 5.000,00")
    .New().AddLabelValue("Prazo", "36 mes(es)")
    .New().AddLabelValue("Id", "{0}", asset => asset.AssetId.ToString())
    .Build();
    
arrayMuitoLouco.Dump();
JsonConvert.SerializeObject(new {arrayMuitoLouco}).Dump();

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


public class Asset
{
    public int AssetId { get; set; }
    public string AssetName { get; set; }
}

//Builder<T>

//new Builder(asset);

//builder
//    .New().AddLableValue("Aplicação minima", $"'{obj.MinimumAmount:'R$ '#,##0.00} ")
//    .New().AddLableValue("Aplicação minima", "'R$ '#,##0.00", asset => asset.minimumAmount)
//    .Build(): string[][]
    
   