// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public readonly struct Email 
    : IEquatable<Email>, IComparable<Email>
{
    private static readonly EmailValidator validator = new();

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public string AsString() => Value;

    public override string ToString() => Value;

    public bool Equals(Email other) => other.Value == Value;

    public override bool Equals(object? other) =>
        other is Email email && Equals(email);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool IsValue(string value) =>
        validator.IsValid(value);

    public static Email From(string value)
    {
        if (!IsValue(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        return new Email(value);
    }

    public int CompareTo(Email other) => 
        Value.CompareTo(other.Value);

    public static bool operator ==(Email lhs, Email rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(Email lhs, Email rhs) =>
        !(lhs == rhs);

    public static bool operator <(Email lhs, Email rhs) =>
        lhs.CompareTo(rhs) < 0;

    public static bool operator <=(Email lhs, Email rhs) =>
        lhs.CompareTo(rhs) <= 0;

    public static bool operator >(Email lhs, Email rhs) =>
        lhs.CompareTo(rhs) > 0;

    public static bool operator >=(Email lhs, Email rhs) =>
        lhs.CompareTo(rhs) >= 0;
}