// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public static class MiscValidators
{
    public static MustBe<T> Valid<T>(this MustBe<T> mb, Func<T, bool> isValid)
    {
        if (isValid is null)
        {
            throw new InvalidOperationException(
                "An \"isValid\" must be supplied.");
        }

        return mb.ThrowIf(
            v => isValid(v),
            v => "Value does not fall within the expected range.");
    }
}