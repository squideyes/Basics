// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using Vogen;

namespace SquidEyes.Basics;

[ValueObject(typeof(TimeOnly), conversions: Conversions.None)]
[JsonConverter(typeof(JsonStringMomentConverter))]
[TypeConverter(typeof(MomentTypeConverter))]
public partial struct Moment : IComparable<Moment>
{
    public string ToString(bool hourAndMinuteOnly = false)
    {
        var sb = new StringBuilder();

        sb.Append(Value.Hour.ToString("00"));
        sb.Append(':');
        sb.Append(Value.Minute.ToString("00"));

        if (hourAndMinuteOnly)
            return sb.ToString();

        sb.Append(':');
        sb.Append(Value.Second.ToString("00"));
        sb.Append('.');
        sb.Append(Value.Millisecond.ToString("000"));

        return sb.ToString();
    }

    public int Hour => Value.Hour;
    public int Minute => Value.Minute;
    public int Second => Value.Second;
    public int Millisecond => Value.Millisecond;

    public TimeSpan AsTimeSpan() =>
        new(0, Hour, Minute, Second, Millisecond);

    public TimeOnly AsTimeOnly() => Value;

    public int CompareTo(Moment other) =>
        Value.CompareTo(other.Value);

    public static Moment MinValue =>
        From(TimeOnly.MinValue);

    public static Moment MaxValue =>
        From(TimeOnly.MaxValue);

    public static Moment Parse(string value) =>
        From(TimeOnly.Parse(value));

    public static bool IsValid(string value) =>
        TimeOnly.TryParse(value, out var _);

    public static Moment From(TimeSpan value) =>
        From(TimeOnly.FromTimeSpan(value));

    public static Moment From(DateTime value) =>
        From(TimeOnly.FromDateTime(value));

    public static Moment From(int hours, int minutes,
        int seconds = 0, int milliseconds = 0)
    {
        return From(new TimeOnly(
            hours, minutes, seconds, milliseconds));
    }

    public static bool operator <(Moment lhs, Moment rhs) =>
        lhs.Value.CompareTo(rhs.Value) < 0;

    public static bool operator <=(Moment lhs, Moment rhs) =>
        lhs.Value.CompareTo(rhs.Value) <= 0;

    public static bool operator >(Moment lhs, Moment rhs) =>
        lhs.Value.CompareTo(rhs.Value) > 0;

    public static bool operator >=(Moment lhs, Moment rhs) =>
        lhs.Value.CompareTo(rhs.Value) >= 0;
}