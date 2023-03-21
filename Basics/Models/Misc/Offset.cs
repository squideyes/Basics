// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public readonly struct Offset
{
    public Offset(Vector vector, float points)
    {
        Vector = vector;
        Points = points;
    }

    public Vector Vector { get; }
    public float Points { get; }

    public override string ToString() => $"{Vector}={Points}";

    internal float AsSignedPoints() =>
        Points * (Vector == Vector.Plus ? 1.0f : -1.0f);

    public static Offset From(Vector vector, float points)
    {
        vector.Must().BeEnumValue();
        points.Must().BePositiveOrZero();

        return new Offset(vector, points);
    }

    public static Offset Parse(string value)
    {
        value.MayNot().BeNullOrWhitespace();

        var fields = value.Split('=');

        fields.Length.Must().Be(2);

        var vector = Enum.Parse<Vector>(fields[0], true);
        var points = float.Parse(fields[1]).Must().BePositiveOrZero();

        return From(vector, points);
    }
}