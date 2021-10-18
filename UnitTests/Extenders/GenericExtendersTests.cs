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
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class GenericExtendersTests
    {
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, false)]
        [InlineData(2, 1, false)]
        [InlineData(3, 1, true)]
        public void HasMaskBitsShouldWorkForBytes(byte value, byte mask, bool expected) =>
            value.HasMaskBits(mask).Should().Be(expected);

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, false)]
        [InlineData(2, 1, false)]
        [InlineData(3, 1, true)]
        public void HasMaskBitsShouldWorkForInts(int value, int mask, bool expected) =>
            value.HasMaskBits(mask).Should().Be(expected);

        [Fact]
        public void IsDefaultValueWorkForIConvertbleTypes()
        {
            default(bool).IsDefaultValue().Should().BeTrue();
            default(byte).IsDefaultValue().Should().BeTrue();
            default(char).IsDefaultValue().Should().BeTrue();
            default(short).IsDefaultValue().Should().BeTrue();
            default(int).IsDefaultValue().Should().BeTrue();
            default(long).IsDefaultValue().Should().BeTrue();
            default(sbyte).IsDefaultValue().Should().BeTrue();
            default(ushort).IsDefaultValue().Should().BeTrue();
            default(uint).IsDefaultValue().Should().BeTrue();
            default(ulong).IsDefaultValue().Should().BeTrue();
            default(DateTime).IsDefaultValue().Should().BeTrue();
            default(decimal).IsDefaultValue().Should().BeTrue();
            default(float).IsDefaultValue().Should().BeTrue();
            default(double).IsDefaultValue().Should().BeTrue();
            default(UriKind).IsDefaultValue().Should().BeTrue();
        }

        private static List<int> ParseChoices(string choices) =>
            choices.Split(',').Select(c => int.Parse(c)).ToList();

        [Theory]
        [InlineData("1,2,3", 2, true)]
        [InlineData("", 2, false)]
        [InlineData("1,2,3", 4, false)]
        public void InWorksWithIEnumerable(string choices, int value, bool expected) =>
            value.In(ParseChoices(choices)).Should().Be(expected);

        [Theory]
        [InlineData("1,2,3", 2, true)]
        [InlineData("1,2,3", 4, false)]
        public void InWorksWithNonEmptyHashSet(string choices, int value, bool expected) =>
            value.In(new HashSet<int>(ParseChoices(choices))).Should().Be(expected);

        [Fact]
        public void InWorksWithEmptyHashSet() => 1.In(new HashSet<int>()).Should().Be(false);

        [Fact]
        public void InWorksWithParams() => 1.In(1, 2, 3).Should().BeTrue();

        [Theory]
        [InlineData(UriKind.Absolute, "ABSOLUTE")]
        [InlineData(UriKind.Relative, "RELATIVE")]
        public void EnumToUpperWorks(UriKind uriKind, string expected) =>
            uriKind.ToUpper().Should().Be(expected);

        [Theory]
        [InlineData(UriKind.Absolute, "absolute")]
        [InlineData(UriKind.Relative, "relative")]
        public void EnumToLowerWorks(UriKind uriKind, string expected) =>
            uriKind.ToLower().Should().Be(expected);

        [Theory]
        [InlineData(1, 1, 3, true, true)]
        [InlineData(2, 1, 3, true, true)]
        [InlineData(3, 1, 3, true, true)]
        [InlineData(1, 1, 3, false, false)]
        [InlineData(2, 1, 3, false, true)]
        [InlineData(3, 1, 3, false, false)]
        public void BetweenWorks<T>(T value, T minValue, T maxValue,
            bool exclusive, bool expected) where T : IComparable
        {
            value.Between(minValue, maxValue, exclusive).Should().Be(expected);
        }

        [Fact]
        public void BetweenThrowsErrorWithBadMinMax()
        {
            FluentActions.Invoking(() => 2.Between(3, 2)).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void ValidatedWithGoodValueShouldReturnValue() =>
            2.Validated("Code", v => v == 2).Should().Be(2);

        [Fact]
        public void ValidatedWithGoodValueShouldReturnConvertedValue() =>
            2.Validated("Code", v => v == 2, v=> v.ToString()).Should().Be("2");

        [Fact]
        public void ValidatedWithBadFieldNameShouldThrowError()
        {
            FluentActions.Invoking(() => 2.Validated("", v => v == 2))
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ValidatedWithBadValueShouldThrowError()
        {
            FluentActions.Invoking(() => 2.Validated("Code", v => v != 2))
                .Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
