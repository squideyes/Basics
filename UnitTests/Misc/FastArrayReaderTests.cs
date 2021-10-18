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
using System.IO;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class FastArrayReaderTests
    {
        [Fact]
        public void AllTypesShouldBeReadable()
        {
            var stream = new MemoryStream();

            var writer = new BinaryWriter(stream);

            writer.Write(sbyte.MaxValue);
            writer.Write(byte.MaxValue);
            writer.Write(short.MaxValue);
            writer.Write(int.MaxValue);
            writer.Write(ushort.MaxValue);
            writer.Write(float.MaxValue);

            var reader = new FastArrayReader(stream.ToArray());

            reader.ReadSByte().Should().Be(sbyte.MaxValue);
            reader.ReadByte().Should().Be(byte.MaxValue);
            reader.ReadInt16().Should().Be(short.MaxValue);
            reader.ReadInt32().Should().Be(int.MaxValue);
            reader.ReadUInt16().Should().Be(ushort.MaxValue);
            reader.ReadSingle().Should().Be(float.MaxValue);    
        }
    }
}
