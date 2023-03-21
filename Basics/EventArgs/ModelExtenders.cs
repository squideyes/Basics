// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Bankroll.Models;

namespace SquidEyes.Basics;

public static class ModelExtenders
{
    public static string ToCode(this RunMode mode) =>
        mode.ToString()[0].ToString();

    public static RunMode ToRunMode(this string value)
    {
        return value switch
        {
            "L" => RunMode.Live,
            "T" => RunMode.Test,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
}