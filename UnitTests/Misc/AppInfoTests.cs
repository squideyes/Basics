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
using Xunit;

namespace SquidEyes.UnitTests
{
    public class AppInfoTests
    {
        [Fact]
        public void AppInfoConstructsAsExpected()
        {
            var assembly = typeof(AppInfoTests).Assembly;

            var appInfo = new AppInfo(assembly);

            appInfo.Company.Should().Be("SquidEyes, LLC");
            appInfo.PackageId.Should().Be(assembly.GetName().Name);
            appInfo.Copyright.Should().Be("Copyright 2021 by SquidEyes, LLC");
            appInfo.Product.Should().Be(assembly.GetName().Name);
            appInfo.Version.Should().Be(new System.Version(1, 0, 0, 0));
            appInfo.Title.Should().Be("SquidEyes.UnitTests v1.0");
        }

        [Theory]
        [InlineData("SomeApp", "1.0", "SomeApp v1.0")]
        [InlineData("SomeApp", "1.0.0", "SomeApp v1.0")]
        [InlineData("SomeApp", "1.0.0.0", "SomeApp v1.0")]
        [InlineData("SomeApp", "1.0.1", "SomeApp v1.0.1")]
        [InlineData("SomeApp", "1.0.0.1", "SomeApp v1.0.0.1")]
        public void GetTitleReturnsExpectedValue(
            string packageId, string versionString, string title)
        {
            AppInfo.GetTitle(packageId, 
                System.Version.Parse(versionString)).Should().Be(title);
        }
    }
}
