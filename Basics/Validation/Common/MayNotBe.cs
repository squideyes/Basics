// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public class MayNotBe<T>
{
    protected const string PREFIX = $"Value may not be ";

    private readonly Func<T, bool> canEval;

    internal MayNotBe(T value, string argName, Func<T, bool> canEval)
    {
        Value = value;
        ArgName = argName!;
        this.canEval = canEval;
    }

    internal T Value { get; }
    protected string ArgName { get; private set; }
    protected string? Message { get; private set; }

    public MayNotBe<T> WithArgName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException();

        ArgName = value;

        return this;
    }

    public MayNotBe<T> WithMessage(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException();

        Message = value;

        return this;
    }

    internal MayNotBe<T> ThrowIf(
        Func<T, bool> isValid, Func<T, string> getSuffix)
    {
        return ThrowIf(isValid, $"{PREFIX}{getSuffix(Value)}.");
    }

    internal MayNotBe<T> ThrowIf(Func<T, bool> isValid, string message)
    {
        if (canEval(Value) && !isValid(Value))
            throw new ValidationException(ArgName, Message ?? message);

        return this;
    }

    public static implicit operator T(MayNotBe<T> mustBe) => mustBe.Value;
}