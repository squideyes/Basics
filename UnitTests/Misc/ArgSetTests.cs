// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System.Text.Json;
using Xunit;

namespace SquidEyes.UnitTests;

public class ArgSetTests
{
    [Fact]
    public void StringValueWithGetInt32()
    {
        var args = new ArgSet
        {
            { Key.From("Value"), "ABC123" }
        };

        FluentActions.Invoking(() => args.Get<int>(Key.From("Value")))
            .Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void ArgSetRoundTripThroughJson()
    {
        var source = new ArgSet
        {
            { "Boolean", true },
            { "Byte", byte.MaxValue },
            { "DateOnly", DateOnly.MaxValue },
            { "DateTime", DateTime.MaxValue },
            { "Decimal", decimal.MaxValue },
            { "Double", double.MaxValue },
            { "Email", EmailAddress.From("some@dude.com")},
            { "Enum", UriKind.Absolute },
            { "Float", float.MaxValue },
            { "Guid", Guid.NewGuid() },
            { "Int16", short.MaxValue },
            { "Int32", int.MaxValue },
            { "Int64", long.MaxValue },
            { "Name", Token.From("SomeName") },
            { "Phone", PhoneNumber.From("215-316-8538") },
            { "Quantity", Quantity.From(123) },
            { "Ratchet", GetRatchet() },
            { "String", "ABC123" },
            { "TimeOnly", TimeOnly.MaxValue },
            { "TimeSpan", TimeSpan.MaxValue },
            { "Uri", new Uri("http://cnn.com") }
        };

        var options = new JsonSerializerOptions();

        options.Converters.Add(new JsonStringArgSetConverter());

        var json = JsonSerializer.Serialize(source, options);

        var target = JsonSerializer.Deserialize<ArgSet>(json, options);

        void Validate<T>(string name)
        {
            var lhs = source.Get<T>(name)!;
            var rhs = target!.Get<T>(name)!;

            lhs.Should().Be(rhs);
        }

        Validate<bool>("Boolean");
        Validate<byte>("Byte");
        Validate<DateOnly>("DateOnly");
        Validate<DateTime>("DateTime");
        Validate<decimal>("Decimal");
        Validate<double>("Double");
        Validate<EmailAddress>("Email");
        Validate<UriKind>("Enum");
        Validate<float>("Float");
        Validate<Guid>("Guid");
        Validate<short>("Int16");
        Validate<int>("Int32");
        Validate<long>("Int64");
        Validate<Token>("Name");
        Validate<PhoneNumber>("Phone");
        Validate<Quantity>("Quantity");
        Validate<Ratchet>("Ratchet");
        Validate<string>("String");
        Validate<TimeOnly>("TimeOnly");
        Validate<TimeSpan>("TimeSpan");
        Validate<Uri>("Uri");
    }

    private static Ratchet GetRatchet()
    {
        var trigger = Offset.From(Vector.Plus, 3.0f);
        var moveStopTo = Offset.From(Vector.Minus, 0.25f);

        return Ratchet.From(trigger, moveStopTo);
    }
}