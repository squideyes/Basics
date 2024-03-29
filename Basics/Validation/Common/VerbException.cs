// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public class VerbException : ArgumentException
{
    public VerbException(string argame, string message)
        : base($"{message} (Argument: \"{argame}\")")
    {
    }
}