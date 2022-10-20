// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Numerics;

namespace SquidEyes.Basics;

public static class NumberValidators
{
    public static MustBe<T> Zero<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowIf(
            v => v == T.Zero,
            v => "zero");
    }

    public static MayNotBe<T> Zero<T>(this MayNotBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowIf(
            v => v != T.Zero,
            v => "zero");
    }

    public static MustBe<T> Positive<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowIf(
            v => v > T.Zero,
            v => "greater than zero");
    }

    public static MustBe<T> PositiveOrZero<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowIf(
            v => v >= T.Zero,
            v => "greater than or equal to zero");
    }

    public static MustBe<T> Negative<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowIf(
            v => v < T.Zero,
            v => "less than zero");
    }

    public static MustBe<T> NegativeOrZero<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowIf(
            v => v <= T.Zero,
            v => "less than or equal to zero");
    }

    public static MustBe<T> Between<T>(
        this MustBe<T> m, T minValue, T maxValue)
        where T : INumber<T>
    {
        if (maxValue < minValue)
        {
            throw new InvalidOperationException(
                "\"maxValue\" must be >= \"minValue.");
        }

        return m.ThrowIf(
            v => v >= minValue && v <= maxValue,
            $">= {minValue} and <= {maxValue}");
    }
}