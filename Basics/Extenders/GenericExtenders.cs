// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com) 
// 
// This file is part of SquidEyes.Basics
// 
// The use of this source code is licensed under the terms 
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

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
