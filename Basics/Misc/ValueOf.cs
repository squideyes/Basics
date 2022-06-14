using System.Linq.Expressions;
using System.Reflection;

namespace SquidEyes.Basics;

public class ValueOf<V, T> where T : ValueOf<V, T>, new()
{
    private static readonly Func<T> factory;

    protected virtual void Validate()
    {
    }

    protected virtual bool TryValidate()
    {
        return true;
    }

    static ValueOf()
    {
        ConstructorInfo ctor = typeof(T)
            .GetTypeInfo()
            .DeclaredConstructors
            .First();

        var args = Array.Empty<Expression>();

        var body = Expression.New(ctor, args);

        var lambda = Expression.Lambda(typeof(Func<T>), body);

        factory = (Func<T>)lambda.Compile();
    }

    public V Value { get; protected set; }

    public static T From(V value)
    {
        T instance = factory();

        instance.Value = value;

        instance.Validate();

        return instance;
    }

    public static bool TryFrom(V value, out T result)
    {
        T instance = factory();

        instance.Value = value;

        result = instance.TryValidate() ? instance : null!;

        return result != null!;
    }

    protected virtual bool Equals(ValueOf<V, T> other) =>
        EqualityComparer<V>.Default.Equals(Value, other.Value);

    public override bool Equals(object? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (other.GetType() != GetType())
            return false;
        
        return Equals((ValueOf<V, T>)other);
    }

    public override string ToString() => Value!.ToString()!;

    public override int GetHashCode() =>
        EqualityComparer<V>.Default.GetHashCode(Value!);

    public static bool operator ==(ValueOf<V, T> left, ValueOf<V, T> right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(ValueOf<V, T> left, ValueOf<V, T> right) =>
        !(left == right);
}