// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public class Key : IEquatable<Key>
{
    private Key(string value)
    {
        Value = value;
    }

    private string Value { get; }

    public string AsString() => Value;

    public string[] AsFields() => Value.Split(':');

    public static Key From(string value)
    {
        value.MayNot().BeNullOrWhitespace();

        var fields = value.Split(':');

        foreach (var field in fields)
            value.Must().Be(v => Name.IsValue(field));

        return new Key(value);
    }

    public override string ToString() => Value;

    public bool Equals(Key? other) =>
        other is not null && Value == other.Value;

    public override bool Equals(object? other) =>
        other is Key key && Equals(key);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Key lhs, Key rhs)
    {
        if (lhs is null)
            return rhs is null;

        return lhs.Equals(rhs);
    }

    public static bool operator !=(Key lhs, Key rhs) =>
        !(lhs == rhs);

    public static implicit operator Key(string value) => From(value);
}