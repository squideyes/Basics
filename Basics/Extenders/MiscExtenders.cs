// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Numerics;
using System.Text;
using static System.DayOfWeek;

namespace SquidEyes.Basics;

public static class MiscExtenders
{
    public static void DoIfCanDo<T>(this T value, Func<T, bool> canDo, Action<T> @do)
    {
        if (canDo(value))
            @do(value);
    }

    public static R Get<T, R>(this T value, Func<T, R> func) => func(value);

    public static void Act<T>(this T value, Action<T> action) => action(value);

    public static bool IsTrue<T>(this T value)
          where T : INumber<T>
    {
        return value != T.Zero;
    }

    public static bool IsFalse<T>(this T value)
        where T : INumber<T>
    {
        return value == T.Zero;
    }

    public static void AppendFolder(this StringBuilder sb, object value)
    {
        if (sb.Length > 0)
            sb.Append(Path.DirectorySeparatorChar);

        sb.Append(value);
    }

    public static bool IsWeekDay(this DayOfWeek value) =>
        value >= Monday && value <= Friday;

    public static bool IsBetween<T>(this T value, T minValue, T maxValue)
        where T : IComparable<T>
    {
        return value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0;
    }
}