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
    public class EnumExtendersTests
    {
        private enum Color
        {
            Red = 1,
            Green,
            Blue
        }

        [Flags]
        public enum Flags
        {
            One = 1,
            Two = 2
        }

        [Theory]
        [InlineData(Color.Red, true)]
        [InlineData((Color)0, false)]
        [InlineData(Flags.One, true)]
        [InlineData(Flags.Two, true)]
        [InlineData(Flags.One | Flags.Two, false)]
        public void IsEnumValueWorksAsExpected<T>(T enumeration, bool expected)
            where T : struct, Enum
        {
            enumeration.IsEnumValue().Should().Be(expected);
        }

        [Theory]
        [InlineData(Flags.One, true)]
        [InlineData(Flags.Two, true)]
        [InlineData(Flags.One | Flags.Two, true)]
        [InlineData((Flags)0, false)]
        [InlineData((Flags)4, false)]
        public void IsFlagsValueWorksAsExpected(Flags flags, bool expected) =>
            flags.IsFlagsValue().Should().Be(expected);
    }
}
