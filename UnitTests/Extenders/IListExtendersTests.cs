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
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System;

namespace SquidEyes.UnitTests
{
    public class IListExtendersTests
    {
        public enum Validation
        {
            None,
            Success,
            Failure,
            Lambda
        }

        [Theory]
        [InlineData("1,2,3", Validation.None, true)]
        [InlineData("1,2,3", Validation.Success, true)]
        [InlineData("1,2,3", Validation.Failure, false)]
        [InlineData("1,2,3", Validation.Lambda, true)]
        [InlineData("1,2,4", Validation.Lambda, false)]
        [InlineData("", Validation.None, false)]
        [InlineData("", Validation.Success, false)]
        [InlineData("0,2,3", Validation.None, false)]
        [InlineData("0,2,3", Validation.Success, false)]
        public void HasNonDefaultItemsShouldWorkAsExpected(
            string values, Validation validation, bool expected)
        {
            List<int> items;

            if (values == "")
                items = new List<int>();
            else
                items = values.Split(',').Select(v => int.Parse(v)).ToList();

            Func<int, bool>? isValid = validation switch
            {
                Validation.None => null,
                Validation.Success => v => true,
                Validation.Failure => v => false,
                Validation.Lambda => v => v <= 3,
                _ => throw new ArgumentOutOfRangeException(nameof(validation))
            };

            items.HasNonDefaultItems(isValid).Should().Be(expected);
        }
    }
}
