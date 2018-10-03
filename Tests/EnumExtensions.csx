using System.ComponentModel;

public class DatabaseValueAttribute : Attribute
{
    public object Value { get; }

    public DatabaseValueAttribute(string value)
    {
        Value = value;
    }

    public DatabaseValueAttribute(int value)
    {
        Value = value;
    }
}

public static class EnumExtensions
{
    #region EnumTo

    public static int ToInt(Enum value)
    {
        return (int)(object)value;
    }

    public static string ToString(Enum value)
    {
        return value.ToString();
    }

    public static string ToDescription(Enum value)
    {
        return GetAttributeFromEnum<DescriptionAttribute>(value)?.Description;
    }

    public static string ToDatabaseValue(Enum value)
    {
        return GetAttributeFromEnum<DatabaseValueAttribute>(value)?.Value?.ToString();
    }

    #endregion

    #region From

    public static T FromInt<T>(int value) where T : struct
    {
        return (T)Enum.ToObject(typeof(T), value);
    }

    public static T FromName<T>(string value) where T : struct
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static T? FromDescription<T>(string value) where T : struct
    {
        return GetEnumFromAttribute<T, DescriptionAttribute>(value, obj => obj?.ToString());
    }

    public static T? FromDatabaseValue<T>(string value) where T : struct
    {
        return GetEnumFromAttribute<T, DatabaseValueAttribute>(value, obj => obj?.ToString());
    }

    public static T? FromDatabaseValue<T>(int value) where T : struct
    {
        return GetEnumFromAttribute<T, DatabaseValueAttribute>(value, obj => (int)obj);
    }

    #endregion

    private static T? GetEnumFromAttribute<T, TAttribute>(object value, Func<object, object> parser) 
        where T : struct
        where TAttribute : Attribute
    {
        var enumItem = typeof(T)
            .GetMembers()
            .Where(item => Attribute.IsDefined(item, typeof(TAttribute)))
            .Select(item => new
            {
                Value = (item as FieldInfo)?.GetValue(null),
                AttrValue = item?.CustomAttributes?.FirstOrDefault(ca => ca.AttributeType == typeof(TAttribute))?.ConstructorArguments?.FirstOrDefault().Value
            })
            .FirstOrDefault(item => item.AttrValue != null && parser(item.AttrValue).Equals(value));

        return enumItem?.Value != null ? (T)enumItem.Value : (T?)null;
    }

    private static TType GetAttributeFromEnum<TType>(Enum value) where TType : Attribute
    {
        var result = value.GetType()
            .GetMembers()
            .Where(w => Attribute.IsDefined(w, typeof(TType)))
            .Select(item => new
            {
                Value = (item as FieldInfo)?.GetValue(null),
                AttrValue = (Attribute.GetCustomAttribute(item, typeof(TType)) as TType)
            })
            .FirstOrDefault(item => item.AttrValue != null && item.Value.Equals(value));

        return result?.AttrValue;
    }
}


public enum Side
{
    [Description("Compra")]
    [DatabaseValue("C")]
    Buy = 1,
    
    [Description("Venda")]
    [DatabaseValue("V")]
    Sell = 2
}

var someSide = Side.Sell;

EnumExtensions.ToInt(someSide).Dump();
EnumExtensions.ToString(someSide).Dump();
EnumExtensions.ToDescription(someSide).Dump();
EnumExtensions.ToDatabaseValue(someSide).Dump();
EnumExtensions.FromInt<Side>(2).Dump();
EnumExtensions.FromName<Side>("Sell").Dump();
EnumExtensions.FromDescription<Side>("Venda").Dump();
EnumExtensions.FromDatabaseValue<Side>("V").Dump();

