// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Basics;
using System;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class EquivilenceValidatorsTests
    {
        private static void InvokeMust<T>(T value, 
            Action<MustBe<T>> action, bool errorExpected)
        {
            try
            {
                action(value.MustBe());
            }
            catch
            {
                if (!errorExpected)
                    throw;
            }
        }

        [Fact]
        public void X()
        {
        }

        [Fact]
        public void NullWithVariousValues()
        {
            InvokeMust("ABC", m => m.Null(), true);
            InvokeMust((string)null!, m => m.Null(), false);
            InvokeMust((object)null!, m => m.Null(), false);
        }

        [Fact]
        public void NotNullWithVariousValues()
        {
            InvokeMust("ABC", m => m.NotNull(), false);
            InvokeMust((string)null!, m => m.NotNull(), true);
            InvokeMust((object)null!, m => m.NotNull(), true);
        }
    }
}