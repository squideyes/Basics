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

namespace SquidEyes.Basics;

public static class GenericExtenders
{
    public static bool HasMaskBits(this byte value, byte mask) =>
        (value & mask) == mask;

    public static bool HasMaskBits(this int value, int mask) =>
        (value & mask) == mask;

    public static bool IsDefaultValue<T>(this T value) =>
        Equals(value, default(T));

    public static T Validated<T>(
        this T value, string fieldName, Func<T, bool> isValid)
    {
        if (string.IsNullOrWhiteSpace(fieldName))
            throw new ArgumentNullException(nameof(fieldName));

        if (isValid(value))
            return value;
        else
            throw new ArgumentOutOfRangeException(fieldName);
    }

    public static R Validated<T, R>(this T value,
        string fieldName, Func<T, bool> isValid, Func<T, R> getResult)
    {
        if (fieldName.IsEmptyOrWhitespace())
            throw new ArgumentNullException(nameof(fieldName));

        if (isValid(value))
            return getResult(value);
        else
            throw new ArgumentOutOfRangeException(nameof(fieldName));
    }

    public static bool In<T>(this T value, params T[] choices) =>
        choices.Contains(value);

    public static bool In<T>(this T value, IEnumerable<T> choices) =>
        choices.Contains(value);

    public static bool In<T>(this T value, HashSet<T> choices) =>
        choices.Contains(value);

    public static bool Between<T>(this T value, T minValue, T maxValue, bool inclusive = true)
        where T : IComparable
    {
        if (maxValue.CompareTo(minValue) < 0)
            throw new ArgumentOutOfRangeException(nameof(maxValue));

        if (inclusive)
            return (value.CompareTo(minValue) >= 0) && (value.CompareTo(maxValue) <= 0);
        else
            return (value.CompareTo(minValue) > 0) && (value.CompareTo(maxValue) < 0);
    }

    public static string ToUpper(this Enum value) => value.ToString().ToUpper();

    public static string ToLower(this Enum value) => value.ToString().ToLower();
}
