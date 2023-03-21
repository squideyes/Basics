// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;

namespace SquidEyes.Basics;

public readonly partial struct KnownAs : IEquatable<KnownAs>, IComparable<KnownAs>
{
    private static readonly Regex validator = GetValidator();

    public KnownAs(string value)
    {
        Value = value;
    }

    private string Value { get; }

    public override string ToString() => Value;

    public string AsString() => Value;

    public bool Equals(KnownAs other) => Value == other.Value;

    public override bool Equals(object? other) =>
        other is KnownAs knownAs && Equals(knownAs);

    public override int GetHashCode() => Value.GetHashCode();
    
    public int CompareTo(KnownAs other) => 
        Value.CompareTo(other.Value);

    public static KnownAs From(string value)
    {
        if (!IsValue(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        return new KnownAs(value);
    }

    public static bool IsValue(string value) =>
        validator.IsMatch(value);

    [GeneratedRegex("^[A-Z][A-Za-z0-9]{0,23}$")]
    private static partial Regex GetValidator();

    public static bool operator ==(KnownAs lhs, KnownAs rhs)=>
        lhs.Equals(rhs);

    public static bool operator !=(KnownAs lhs, KnownAs rhs)=>
        !(lhs == rhs);
}