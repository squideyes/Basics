// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public static class EquivilenceValidators
{
    public static MustBe<T> Null<T>(this MustBe<T> m)
        where T : class
    {
        return m.ThrowIf(
            v => v is null,
            v => "null");
    }

    public static MayNotBe<T> Null<T>(this MayNotBe<T> m)
        where T : class
    {
        return m.ThrowIf(
            v => v is not null,
            v => "null");
    }

    public static MustBe<T> Default<T>(this MustBe<T> m)
    {
        return m.ThrowIf(
            v => v.IsDefault(),
            v => $"a default({typeof(T).FullName})");
    }

    public static MayNotBe<T> Default<T>(this MayNotBe<T> m)
    {
        return m.ThrowIf(
            v => !v.IsDefault(),
            v => $"a default({typeof(T).FullName})");
    }

    public static MustBe<T> EqualTo<T>(this MustBe<T> m, T other)
        where T : IEquatable<T>
    {
        return m.ThrowIf(
            v => EqualityHelper.IsEqualTo(v, other),
            v => $"equal to \"{other}\".");
    }

    public static MayNotBe<T> EqualTo<T>(this MayNotBe<T> m, T other)
        where T : IEquatable<T>
    {
        return m.ThrowIf(
            v => !EqualityHelper.IsEqualTo(v, other),
            v => $"equal to \"{other}\".");
    }
}