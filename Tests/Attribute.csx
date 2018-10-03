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
    [Description("Compra")]
    [DataBaseValue("C")]
    Buy = 1,
    
    [Description("Venda")]
    [DataBaseValue("V")]
    Sell = 2
}

var someSide = Side.Sell;

var result = someSide.GetType()
                .GetMembers()
                .Where(item => Attribute.IsDefined(item, typeof(DataBaseValueAttribute)))
                .Select(item => new
                {
                    Value = (item as FieldInfo)?.GetValue(null),
                    Attribute = item?.CustomAttributes//?.FirstOrDefault(ca => ca.AttributeType == typeof(DataBaseValueAttribute))?.ConstructorArguments?.FirstOrDefault().Value
                })
                .FirstOrDefault(item => item.Attribute != null);
   
   //someSide.Dump();
   
   //result?.Value.Dump();
   
   //result?.AttrValue.Dump();
   
   //((int)result?.Value).Dump();
   
   result.Dump();