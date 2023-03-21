// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public readonly struct Quantity : IEquatable<Quantity>
{
    public const int MinValue = 1;
    public const int MaxValue = 999;

    private Quantity(int value)
    {
        Value = value;
    }

    private int Value { get; }

    public int AsInt32() => Value;

    public bool Equals(Quantity other) => Value.Equals(other.Value);

    public int CompareTo(Quantity other) => Value.CompareTo(other.Value);

    public override bool Equals(object? other) =>
        other is Quantity quantity && Equals(quantity);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();

    public static Quantity From(int value) =>
        new(value.Must().Be(IsValue));

    public static bool IsValue(int value) =>
        value.IsBetween(MinValue, MaxValue);

    public static bool operator ==(Quantity lhs, Quantity rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(Quantity lhs, Quantity rhs) =>
        !(lhs == rhs);
}