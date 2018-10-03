using System.ComponentModel;

public class DataBaseValueAttribute : Attribute
{
    public object Value { get; }

    public DataBaseValueAttribute(string value)
    {
        Value = value;
    }

    public DataBaseValueAttribute(int value)
    {
        Value = value;
    }
}


public enum Side
{
    [Description("Buy")]
    [DataBaseValue("C")]
    Buy = 1,
    
    [Description("Sell")]
    [DataBaseValue("V")]
    Sell = 2
}

var someSide = Side.Buy;

typeof(Side)
    .GetMembers()
    .ToList()
    .Where(w => Attribute.IsDefined(w, typeof(DataBaseValueAttribute)))
    .Select(s => new 
    {
        Member = s,
        AttrValue = (Attribute.GetCustomAttribute(s, typeof(DataBaseValueAttribute)) as DataBaseValueAttribute)?.Value
    })
    .Where(w => w.AttrValue != null)
    .Aggregate("Valores possiveis são ", (ac, i) => ac + i.AttrValue + ", ")
    .Dump();
    
    
    //IEnumerable<string> test = null;
    //test.Any().Dump();