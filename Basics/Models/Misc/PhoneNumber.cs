// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using PhoneNumbers;

namespace SquidEyes.Basics;

public readonly struct PhoneNumber 
    : IEquatable<PhoneNumber>, IComparable<PhoneNumber>
{
    private static readonly PhoneNumberUtil util =
        PhoneNumberUtil.GetInstance();

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public string AsString() => Value;

    public override string ToString() => Value;

    public bool Equals(PhoneNumber other) => other.Value == Value;

    public override bool Equals(object? other) =>
        other is PhoneNumber email && Equals(email);

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

    public static PhoneNumber From(string value)
    {
        if (!IsValue(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        return new PhoneNumber(value);
    }

    public int CompareTo(PhoneNumber other) =>
        Value.CompareTo(other.Value);

    public static bool operator ==(PhoneNumber lhs, PhoneNumber rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(PhoneNumber lhs, PhoneNumber rhs) =>
        !(lhs == rhs);

    public static bool operator <(PhoneNumber lhs, PhoneNumber rhs) =>
        lhs.CompareTo(rhs) < 0;

    public static bool operator <=(PhoneNumber lhs, PhoneNumber rhs) =>
        lhs.CompareTo(rhs) <= 0;

    public static bool operator >(PhoneNumber lhs, PhoneNumber rhs) =>
        lhs.CompareTo(rhs) > 0;

    public static bool operator >=(PhoneNumber lhs, PhoneNumber rhs) =>
        lhs.CompareTo(rhs) >= 0;
}