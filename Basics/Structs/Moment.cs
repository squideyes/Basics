// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com)
//
// This file is part of SquidEyes.Basics
//
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public struct Moment : IEquatable<Moment>, IComparable<Moment>
{
    public Moment()
    {
    }

    private Moment(TimeSpan value) => Value = value;

    public int Hours => Value.Hours;
    public int Minutes => Value.Minutes;
    public int Seconds => Value.Seconds;
    public int Milliseconds => Value.Milliseconds;

    internal TimeSpan Value { get; } = TimeSpan.Zero;

    public TimeSpan AsTimeSpan() => Value;

    public TimeOnly AsTimeOnly() => TimeOnly.FromTimeSpan(Value);

    public override string ToString() => ToString(true);

    public string ToString(bool withSecondsAndMilliseconds)
    {
        if (withSecondsAndMilliseconds)
            return Value.ToString("hh\\:mm\\:ss\\.fff");
        else
            return Value.ToString("hh\\:mm");
    }

    public int CompareTo(Moment other) =>
        Value.CompareTo(other.Value);

    public bool Equals(Moment other) => Value.Equals(other.Value);

    public override bool Equals(object? other) =>
        other is Moment moment && Equals(moment);

    public override int GetHashCode() => Value.GetHashCode();

    public static Moment From(int hours = 0,
        int minutes = 0, int seconds = 0, int milliseconds = 0)
    {
        return From(new TimeSpan(
            0, hours, minutes, seconds, milliseconds));
    }

    public static Moment From(TimeSpan value)
    {
        if (value < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(value));

        if (value >= TimeSpan.FromDays(1))
            throw new ArgumentOutOfRangeException(nameof(value));

        return new Moment(value);
    }

    public static Moment Parse(string value)
    {
        var timeSpan = TimeSpan.Parse(value);

        if (timeSpan < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(value));

        if (timeSpan >= TimeSpan.FromDays(1))
            throw new ArgumentOutOfRangeException(nameof(value));

        return timeSpan.AsFunc(t => 
            From(t.Hours, t.Minutes, t.Seconds, t.Milliseconds));
    }

    public static bool operator ==(Moment lhs, Moment rhs) =>
        lhs.Equals(rhs);

    public static bool operator !=(Moment lhs, Moment rhs) =>
        !(lhs == rhs);

    public static bool operator <(Moment lhs, Moment rhs) =>
        lhs.CompareTo(rhs) < 0;

    public static bool operator <=(Moment lhs, Moment rhs) =>
        lhs.CompareTo(rhs) <= 0;

    public static bool operator >(Moment lhs, Moment rhs) =>
        lhs.CompareTo(rhs) > 0;

    public static bool operator >=(Moment lhs, Moment rhs) =>
        lhs.CompareTo(rhs) >= 0;
}