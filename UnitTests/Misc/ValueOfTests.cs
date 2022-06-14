using FluentAssertions;
using SquidEyes.Basics;
using System;
using System.Collections.Generic;
using Xunit;

namespace SquidEyes.UnitTests;

public class ValueOfTests
{
    private class BasicId : ValueOf<string, BasicId>
    {
    }

    private class ZipCode : ValueOf<string, ZipCode>
    {
    }

    private class Location : ValueOf<(string State, ZipCode ZipCode), Location>
    {
    }

    public class TryValidated : ValueOf<string, TryValidated>
    {
        protected override bool TryValidate() =>
            !string.IsNullOrWhiteSpace(Value);
    }

    public class Validated : ValueOf<string, Validated>
    {
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new ArgumentException("Value cannot be null or empty");
        }
    }


    private class Insensitive : ValueOf<string, Insensitive>
    {
        protected override bool Equals(ValueOf<string, Insensitive> other) =>
            EqualityComparer<string>.Default.Equals(Value.ToLower(), other.Value.ToLower());

        public override int GetHashCode() =>
            EqualityComparer<string>.Default.GetHashCode(Value.ToLower());
    }

    [Fact]
    public void SingleValuedExample()
    {
        var id1 = BasicId.From("ABC123");
        var id2 = BasicId.From("ABC123");
        var id3 = BasicId.From("XYZ987");

        id1.Equals(id2).Should().BeTrue();
        id1.GetHashCode().Should().Be(id2.GetHashCode());

        id1.Equals(id3).Should().BeFalse();
        id1.GetHashCode().Should().NotBe(id3.GetHashCode());
    }

    [Fact]
    public void ValueTupleValuedExample()
    {
        var l1 = Location.From(("NY", ZipCode.From("10025")));
        var l2 = Location.From(("NY", ZipCode.From("10025")));
        var l3 = Location.From(("PA", ZipCode.From("19096")));

        l1.Equals(l2).Should().BeTrue();
        l1.GetHashCode().Should().Be(l2.GetHashCode());

        l1.Equals(l3).Should().BeFalse();
        l1.GetHashCode().Should().NotBe(l3.GetHashCode());
    }

    [Fact]
    public void CaseInsensitiveEquals()
    {
        var x1 = Insensitive.From("ABC123");
        var x2 = Insensitive.From("abc123");
        var x3 = Insensitive.From("XYZ987");

        x1.Equals(x2).Should().BeTrue();
        x1.GetHashCode().Should().Be(x2.GetHashCode());
        (x1 == x2).Should().BeTrue();
        (x1.Value == "ABC123").Should().BeTrue();

        x1.Equals(x3).Should().BeFalse();
        x1.GetHashCode().Should().NotBe(x3.GetHashCode());
        (x1 == x3).Should().BeFalse();
    }

    [Fact]
    public void ToStringReturnsValueToStringForSingleValuedObjects()
    {
        var id = BasicId.From("ASDF12345");

        id.Value.Should().Be(id.ToString());
    }

    [Fact]
    public void ToStringReturnsValueOfTupleForTupleValuedObjects()
    {
        var address1 = Location.From(("NY", ZipCode.From("10025")));

        address1.Value.ToString().Should().Be(address1.ToString());
    }

    [Fact]
    public void TryValidateReturnsFalse()
    {
        bool isValid = TryValidated.TryFrom("", out TryValidated valueObject);

        isValid.Should().BeFalse();
        valueObject.Should().BeNull();
    }

    [Fact]
    public void TryValidateReturnsTrue()
    {
        bool isValid = TryValidated.TryFrom("something", out TryValidated valueObject);

        isValid.Should().BeTrue();
        valueObject.Should().NotBeNull();
        valueObject.Value.Should().Be("something");
    }

    [Fact]
    public void SingleThrowsErrorOnInvalidArg()
    {
        FluentActions.Invoking(() => Validated.From("")).Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Value cannot be null or empty");
    }
}