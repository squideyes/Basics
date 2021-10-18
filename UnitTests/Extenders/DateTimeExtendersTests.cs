// Copyright © 2021 by SquidEyes, LLC
// 
// Permission is hereby granted, free of charge, to any person obtaining 
// a copy of this software and associated documentation files (the “Software”),
// to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
// IN THE SOFTWARE.

using FluentAssertions;
using SquidEyes.Basics;
using System;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class DateTimeExtendersTests
    {
        [Fact]
        public void EasternRoundtripWorks() =>
            Roundtrip(d => d.ToEasternFromUtc(), d => d.ToUtcFromEastern());

        [Fact]
        public void CentralRoundtripWorks() =>
            Roundtrip(d => d.ToCentralFromUtc(), d => d.ToUtcFromCentral());

        [Fact]
        public void MountainRoundtripWorks() =>
            Roundtrip(d => d.ToMountainFromUtc(), d => d.ToUtcFromMountain());

        [Fact]
        public void PacificRoundtripWorks() =>
            Roundtrip(d => d.ToPacificFromUtc(), d => d.ToUtcFromPacific());

        [Theory]
        [InlineData("01/02/2021 03:04:05.006", false)]
        [InlineData("01/02/2021", true)]
        [InlineData("01/02/2021 00:00:00.000", true)]
        public void IsDateWorks(string dateTimeString, bool expected) =>
            DateTime.Parse(dateTimeString).IsDate().Should().Be(expected);

        [Theory]
        [InlineData("10/10/2021", false)]
        [InlineData("10/11/2021", true)]
        [InlineData("10/12/2021", true)]
        [InlineData("10/13/2021", true)]
        [InlineData("10/14/2021", true)]
        [InlineData("10/15/2021", true)]
        [InlineData("10/16/2021", false)]
        public void IsWeekdayShouldWork(string dateTimeString, bool expected) =>
            DateTime.Parse(dateTimeString).IsWeekday().Should().Be(expected);

        [Theory]
        [InlineData(0, true)]
        [InlineData(60, true)]
        [InlineData(1, false)]
        [InlineData(59, false)]
        [InlineData(-1, false)]
        [InlineData(61, false)]
        public void OnIntervalShouldWork(int seconds, bool expected) =>
            new DateTime(2021, 10, 10).AddSeconds(seconds).OnInterval(60).Should().Be(expected);

        private static void Roundtrip(
            Func<DateTime, DateTime> getUnspecified, Func<DateTime, DateTime> getUtc)
        {
            var utcNow = DateTime.UtcNow;

            var unspecified = getUnspecified(utcNow);

            getUtc(unspecified).Should().Be(utcNow);
        }
    }
}
