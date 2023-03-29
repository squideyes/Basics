// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using PhoneNumbers;

namespace SquidEyes.Basics;

public readonly struct Phone 
    : IEquatable<Phone>, IComparable<Phone>
{
    private static readonly PhoneNumberUtil util =
        PhoneNumberUtil.GetInstance();

    private Phone(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public string AsString() => Value;

    public override string ToString() => Value;

    public bool Equals(Phone other) => other.Value == Value;

    public override bool Equals(object? other) =>
        other is Phone email && Equals(email);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool IsValue(string value)
    {
        try
        {
            return util.IsValidNumber(util.Parse(value, "US"));
        }
        catch
        {
            return false;
        }
    }

    public static Phone From(string value)
    {
        if (!IsValue(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        return new Phone(value);
    }

    public int CompareTo(Phone other) =>
        Value.CompareTo(other.Value);

    public static bool operator ==(Phone lhs, Phone rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(Phone lhs, Phone rhs) =>
        !(lhs == rhs);

    public static bool operator <(Phone lhs, Phone rhs) =>
        lhs.CompareTo(rhs) < 0;

    public static bool operator <=(Phone lhs, Phone rhs) =>
        lhs.CompareTo(rhs) <= 0;

    public static bool operator >(Phone lhs, Phone rhs) =>
        lhs.CompareTo(rhs) > 0;

    public static bool operator >=(Phone lhs, Phone rhs) =>
        lhs.CompareTo(rhs) >= 0;
}