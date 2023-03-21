// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using static SquidEyes.Basics.IdHelper;

namespace SquidEyes.Basics;

public readonly struct ClientId : IEquatable<ClientId>
{
    private const int SIZE = 8;

    private static readonly char[] charSet =
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

    private static readonly HashSet<char> hashSet = new(charSet);

    public ClientId(string value)
    {
        Value = value;
    }

    private string Value { get; }

    public bool Equals(ClientId other) => other.Value == Value;

    public override bool Equals(object? other) =>
        other is ClientId clientId && Equals(clientId);

    public override int GetHashCode() => Value.GetHashCode();

    public string AsString() => Value;

    public override string ToString() => Value;

    public static ClientId From(string value) =>
        new(value.Must().Be(IsValue));

    public static ClientId Next() => new(GetRandomId(charSet, SIZE));

    public static bool IsValue(string value) =>
        value?.Length == SIZE && value.All(hashSet.Contains);

    public static bool operator ==(ClientId lhs, ClientId rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(ClientId lhs, ClientId rhs) =>
        !(lhs == rhs);
}