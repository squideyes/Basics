// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;

namespace SquidEyes.Basics;

public readonly partial struct Name : IEquatable<Name>, IComparable<Name>
{
    private static readonly Regex validator = Validator();

    private Name(string value)
    {
        Value = value;
    }

    private string Value { get; }

    public string AsString() => Value;

    public bool Equals(Name other) => Value.Equals(other.Value);

    public int CompareTo(Name other) => Value.CompareTo(other.Value);

    public override bool Equals(object? other) =>
        other is Name name && Equals(name);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();

    public static Name From(string value)
    {
        if (!IsValue(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        return new(value);
    }

    public static bool IsValue(string value) =>
        validator.IsMatch(value);

    public static bool operator ==(Name lhs, Name rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(Name lhs, Name rhs) =>
        !(lhs == rhs);

    public static bool operator <(Name lhs, Name rhs) =>
        lhs.CompareTo(rhs) < 0;

    public static bool operator <=(Name lhs, Name rhs) =>
        lhs.CompareTo(rhs) <= 0;

    public static bool operator >(Name lhs, Name rhs) =>
        lhs.CompareTo(rhs) > 0;

    public static bool operator >=(Name lhs, Name rhs) =>
        lhs.CompareTo(rhs) >= 0;

    public static implicit operator Name(string name) => From(name);

    [GeneratedRegex("^[A-Z][A-Za-z0-9]{0,23}$", RegexOptions.Compiled)]
    private static partial Regex Validator();
}