// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public class MayNot<T> : VerbBase<T, MayNot<T>>
{
    public MayNot(T value, string argName, Func<T, bool> canEval)
        : base(value, argName, canEval, "Value may not")
    {
    }
}