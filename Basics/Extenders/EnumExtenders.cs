// Copyright © 2021 by SquidEyes, LLC
// 
// Permission is hereby granted, free of charge, to any person obtaining 
// a copy of this software and associated documentation files (the “Software”),
// to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
// IN THE SOFTWARE.

using System.ComponentModel;

namespace SquidEyes.Basics;

public static class EnumExtenders
{
    public static T ToEnumValue<T>(this string value) =>
        (T)Enum.Parse(typeof(T), value, true);

    public static bool IsEnumValue<T>(this T value) where T : struct, Enum =>
        Enum.IsDefined(value);

    public static bool HasFlags(this Enum value) => value.GetType()
        .GetCustomAttributes(typeof(FlagsAttribute), false).Any();

    public static bool IsFlagsValue(this Enum value)
    {
        switch (Type.GetTypeCode(Enum.GetUnderlyingType(value.GetType())))
        {
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.SByte:
            case TypeCode.Single:
                if (Convert.ToInt64(value) < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                break;
            default:
                break;
        }

        ulong flags = Convert.ToUInt64(value);

        var values = Enum.GetValues(value.GetType());

        int count = values.Length - 1;

        ulong initialFlags = flags;

        ulong flag = 0;

        while (count >= 0)
        {
            flag = Convert.ToUInt64(values.GetValue(count));

            if ((count == 0) && (flag == 0))
                break;

            if ((flags & flag) == flag)
            {
                flags -= flag;

                if (flags == 0)
                    return true;
            }

            count--;
        }

        if (flags != 0)
            return false;

        if (initialFlags != 0 || flag == 0)
            return true;

        return false;
    }

    public static bool TryParseEnumList<T>(this List<string> values, out List<T> items)
        where T : struct
    {
        items = new List<T>();

        if (values.Count == 0)
            return false;

        var joined = string.Join("", values).Replace(" ", "").Split(',');

        foreach (var value in joined)
        {
            if (!Enum.TryParse(value, true, out T symbol))
                return false;

            items.Add(symbol);
        }

        return items.Count > 0;
    }

    public static string ToDescription(this Enum enumeration)
    {
        var fi = enumeration.GetType().GetField(enumeration.ToString())!;

        var attributes = (DescriptionAttribute[])fi
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
        else
            return enumeration.ToString();
    }
}
