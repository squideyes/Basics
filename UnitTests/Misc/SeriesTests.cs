// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using Xunit;

namespace SquidEyes.UnitTests;

public class SeriesTests
{
    [Fact]
    public void MatchesNinjaBehavior()
    {
        var series = new Series<double>();

        series.Count.Should().Be(0);

        series.Add();

        series.Count.Should().Be(1);
        series.GetWasSet(0).Should().BeFalse();
        series[0].Should().Be(0);

        series[0] = 1;

        series.Count.Should().Be(1);
        series.GetWasSet(0).Should().BeTrue();
        series[0].Should().Be(1);

        series.Add();

        series.Count.Should().Be(2);
        series.GetWasSet(0).Should().BeFalse();
        series[0].Should().Be(0);

        series[1].Should().Be(1);

        series.Reset(1);
        series.GetWasSet(1).Should().BeFalse();
        series[0].Should().Be(0);
    }
}