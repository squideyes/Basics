// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;

namespace SquidEyes.Basics;

public readonly partial struct Token : IEquatable<Token>, IComparable<Token>
{
    private static readonly Regex validator = Validator();

    private Token(string value)
    {
        Value = value;
    }

    private string Value { get; }

    public string AsString() => Value;

    public bool Equals(Token other) => Value.Equals(other.Value);

    public int CompareTo(Token other) => Value.CompareTo(other.Value);

    public override bool Equals(object? other) =>
        other is Token token && Equals(token);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();

    public static Token From(string value)
    {
        if (!IsValue(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        return new(value);
    }

    public static bool IsValue(string value) =>
        validator.IsMatch(value);

    public static bool operator ==(Token lhs, Token rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(Token lhs, Token rhs) =>
        !(lhs == rhs);

    public static bool operator <(Token lhs, Token rhs) =>
        lhs.CompareTo(rhs) < 0;

    public static bool operator <=(Token lhs, Token rhs) =>
        lhs.CompareTo(rhs) <= 0;

    public static bool operator >(Token lhs, Token rhs) =>
        lhs.CompareTo(rhs) > 0;

    public static bool operator >=(Token lhs, Token rhs) =>
        lhs.CompareTo(rhs) >= 0;

    public static implicit operator Token(string token) => From(token);

    [GeneratedRegex("^[A-Z][A-Za-z0-9]{0,23}$", RegexOptions.Compiled)]
    private static partial Regex Validator();
}