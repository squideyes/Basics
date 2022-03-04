// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com)
//
// This file is part of SquidEyes.Basics
//
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System.IO;
using Xunit;

namespace SquidEyes.UnitTests;

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