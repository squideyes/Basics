// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public class MustBe<T>
{
    private const string PREFIX = $"Value must be ";

    private readonly Func<T, bool> canEval;

    internal MustBe(T value, string argName, Func<T, bool> canEval)
    {
        Value = value;
        ArgName = argName!;
        this.canEval = canEval;
    }

    internal T Value { get; }

    protected string ArgName { get; private set; }
    protected string? Message { get; private set; }

    public MustBe<T> WithArgName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException();

        ArgName = value;

        return this;
    }

    public MustBe<T> WithMessage(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException();

        Message = value;

        return this;
    }

    internal MustBe<T> ThrowIf(
        Func<T, bool> isValid, Func<T, string> getSuffix)
    {
        return ThrowIf(isValid, $"{PREFIX}{getSuffix(Value)}.");
    }

    internal MustBe<T> ThrowIf(Func<T, bool> isValid, string message)
    {
        if (canEval(Value) && !isValid(Value))
            throw new ValidationException(ArgName, Message ?? message);

        return this;
    }

    public static implicit operator T(MustBe<T> mustBe) => mustBe.Value;
}