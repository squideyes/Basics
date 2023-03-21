// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public readonly struct EmailAddress 
    : IEquatable<EmailAddress>, IComparable<EmailAddress>
{
    private static readonly EmailValidator validator = new();

    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public string AsString() => Value;

    public override string ToString() => Value;

    public bool Equals(EmailAddress other) => other.Value == Value;

    public override bool Equals(object? other) =>
        other is EmailAddress email && Equals(email);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool IsValue(string value) =>
        validator.IsValid(value);

    public static EmailAddress From(string value)
    {
        if (!IsValue(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        return new EmailAddress(value);
    }

    public int CompareTo(EmailAddress other) => 
        Value.CompareTo(other.Value);

    public static bool operator ==(EmailAddress lhs, EmailAddress rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(EmailAddress lhs, EmailAddress rhs) =>
        !(lhs == rhs);

    public static bool operator <(EmailAddress lhs, EmailAddress rhs) =>
        lhs.CompareTo(rhs) < 0;

    public static bool operator <=(EmailAddress lhs, EmailAddress rhs) =>
        lhs.CompareTo(rhs) <= 0;

    public static bool operator >(EmailAddress lhs, EmailAddress rhs) =>
        lhs.CompareTo(rhs) > 0;

    public static bool operator >=(EmailAddress lhs, EmailAddress rhs) =>
        lhs.CompareTo(rhs) >= 0;
}