// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public static class StringValidators
{
    public static MustBe<string> NullOrEmpty(this MustBe<string> mb)
    {
        return mb.ThrowIf(
            string.IsNullOrEmpty,
            v => "null or empty");
    }

    public static MayNotBe<string> NullOrEmpty(this MayNotBe<string> mb)
    {
        return mb.ThrowIf(
            v => !string.IsNullOrEmpty(v),
            v => "null or empty");
    }

    public static MustBe<string> NullOrWhiteSpace(this MustBe<string> mb)
    {
        return mb.ThrowIf(
            string.IsNullOrWhiteSpace,
            v => "null, empty or comprised entirely of whitespace");
    }

    public static MayNotBe<string> NullOrWhiteSpace(this MayNotBe<string> mb)
    {
        return mb.ThrowIf(
            v => !string.IsNullOrWhiteSpace(v),
            v => "null, empty or comprised entirely of whitespace");
    }
}