// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public class Arg
{
    internal Arg(object value)
    {
        Value = value;
    }

    public object Value { get; }
}