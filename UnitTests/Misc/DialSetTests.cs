// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System;
using Xunit;

namespace SquidEyes.UnitTests;

public class DialSetTests
{
    [Fact]
    public void BoolShouldRountrip() => RoundTrip(false,
        true, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetBool(k));

    [Fact]
    public void ByteShouldRountrip() => RoundTrip(byte.MinValue,
        byte.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetByte(k));

    [Fact]
    public void ShortShouldRountrip() => RoundTrip(short.MinValue,
        short.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetShort(k));

    [Fact]
    public void IntShouldRountrip() => RoundTrip(int.MinValue,
        int.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetInt(k));

    [Fact]
    public void LongShouldRountrip() => RoundTrip(long.MinValue,
        long.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetLong(k));

    [Fact]
    public void FloatShouldRountrip() => RoundTrip(float.MinValue,
        float.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetFloat(k));

    [Fact]
    public void DoubleShouldRountrip() => RoundTrip(double.MinValue,
        double.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetDouble(k));

    [Fact]
    public void EnumShouldRountrip() => RoundTrip(UriKind.Absolute,
        UriKind.Relative, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetEnum<UriKind>(k));

    [Fact]
    public void UriShouldRountrip() => RoundTrip(new Uri("http://aaa.com"),
        new Uri("http://bbb.com"), (c, k, d) => c.Upsert(k, d), (c, k) => c.GetUri(k));

    [Fact]
    public void StringShouldRountrip() => RoundTrip("ABC123",
        "XYZ987", (c, k, d) => c.Upsert(k, d), (c, k) => c.GetString(k));

    [Fact]
    public void DateTimeShouldRountrip() => RoundTrip(DateTime.MinValue,
        DateTime.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetDateTime(k));

    [Fact]
    public void DateOnlyShouldRountrip() => RoundTrip(DateOnly.MinValue,
        DateOnly.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetDateOnly(k));

    [Fact]
    public void TimeOnlyShouldRountrip() => RoundTrip(TimeOnly.MinValue,
        TimeOnly.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetTimeOnly(k));

    [Fact]
    public void TimeSpanShouldRountrip() => RoundTrip(TimeSpan.MinValue,
        TimeSpan.MaxValue, (c, k, d) => c.Upsert(k, d), (c, k) => c.GetTimeSpan(k));

    private static void RoundTrip<T>(T data1, T data2,
        Action<DialSet, string, T> upsert, Func<DialSet, string, T> getValue)
    {
        var dials = new DialSet();

        upsert(dials, "KEY", data1);
        getValue(dials, "KEY").Should().Be(data1);

        upsert(dials, "KEY", data2);
        getValue(dials, "KEY").Should().Be(data2);
    }

    [Fact]
    public void ShouldConvertToDictionary()
    {
        var dials = new DialSet();

        dials.Upsert("AAA", true);
        dials.Upsert("BBB", byte.MinValue);
        dials.Upsert("CCC", short.MinValue);
        dials.Upsert("DDD", int.MinValue);
        dials.Upsert("EEE", long.MinValue);
        dials.Upsert("FFF", float.MinValue);
        dials.Upsert("GGG", double.MinValue);
        dials.Upsert("HHH", UriKind.Absolute);
        dials.Upsert("III", new Uri("http://aaa.com"));
        dials.Upsert("JJJ", "ABC");
        dials.Upsert("KKK", DateTime.MinValue);
        dials.Upsert("LLL", DateOnly.MinValue);
        dials.Upsert("MMM", TimeOnly.MinValue);
        dials.Upsert("NNN", TimeSpan.MinValue);

        var dict = dials.ToDictionary();

        dict["AAA"].Should().Be(true.ToString());
        dict["BBB"].Should().Be(byte.MinValue.ToString());
        dict["CCC"].Should().Be(short.MinValue.ToString());
        dict["DDD"].Should().Be(int.MinValue.ToString());
        dict["EEE"].Should().Be(long.MinValue.ToString());
        dict["FFF"].Should().Be(float.MinValue.ToString());
        dict["GGG"].Should().Be(double.MinValue.ToString());
        dict["HHH"].Should().Be(UriKind.Absolute.ToString().ToString());
        dict["III"].Should().Be(new Uri("http://aaa.com").AbsoluteUri);
        dict["JJJ"].Should().Be("ABC".ToString());
        dict["KKK"].Should().Be(DateTime.MinValue.ToString());
        dict["LLL"].Should().Be(DateOnly.MinValue.ToString("MM/dd/yyyy"));
        dict["MMM"].Should().Be(TimeOnly.MinValue.ToString("HH:mm:ss.fff"));
        dict["NNN"].Should().Be(TimeSpan.MinValue.ToString(@"d\.hh\:mm\:ss\.fff"));
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(1, "?AAA=1")]
    [InlineData(2, "?AAA=1&BBB=2")]
    public void GetQueryStringReturnsExpectedValue(int items, string expected)
    {
        var dials = new DialSet();

        if (items >= 1)
            dials.Upsert("AAA", 1);

        if (items >= 2)
            dials.Upsert("BBB", 2);

        dials.ToQueryString().Should().Be(expected);
    }
}