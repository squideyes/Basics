// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public struct ShortId : IEquatable<ShortId>
{
    private const int SIZE = 22;

    private static readonly char[] charSet =
        "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz23456789".ToCharArray();

    private static readonly HashSet<char> hashSet = new(charSet);

    private ShortId(string value)
    {
        Value = value;
    }

    private readonly string Value;

    public string AsString() => Value;

    public bool Equals(ShortId other) => other.Value == Value;

    public override bool Equals(object? other) =>
        other is ShortId shortGuid && Equals(shortGuid);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static ShortId Next() =>
        new(IdHelper.GetRandomId(charSet, SIZE));

    public static ShortId From(string value) =>
        new(value.Must().Be(IsValue));

    public static bool IsValue(string value) =>
        value?.Length == SIZE && value.All(hashSet.Contains);

    public static bool operator ==(ShortId lhs, ShortId rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(ShortId lhs, ShortId rhs) =>
        !(lhs == rhs);
}