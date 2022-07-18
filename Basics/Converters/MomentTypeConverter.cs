using System.ComponentModel;
using System.Globalization;

namespace SquidEyes.Basics;

public class MomentTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(
        ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(TimeOnly)
            || base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext? context,
        CultureInfo? culture, object value)
    {
        if (value is TimeOnly input)
            return Moment.From(input);

        return base.ConvertFrom(context, culture, value)!;
    }

    public override object? ConvertTo(ITypeDescriptorContext? context,
        CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is Moment moment)
            return moment.Value;

        return base.ConvertTo(context, culture, value, destinationType);
    }
}