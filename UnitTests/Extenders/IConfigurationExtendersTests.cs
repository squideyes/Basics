using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SquidEyes.Basics;
using System.Collections.Generic;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class IConfigurationExtendersTests
    {
        [Fact]
        public void GetAsReturnsExpectedValues()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>> 
                {
                    new KeyValuePair<string, string>("Number", 100.ToString())
                })
                .Build();

            config.GetAs<int>("Number").Should().Be(100);
            config.GetAs<string>("Number").Should().Be("100");
        }
    }
}
