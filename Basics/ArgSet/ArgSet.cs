// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Basics;

public class ArgSet : IEnumerable<KeyValuePair<Key, Arg>>
{
    private readonly Dictionary<Key, Arg> dict = new();

    public bool IsEmpty => dict.Count == 0;

    public bool Contains(Key key) => dict.ContainsKey(key);

    public void Add<T>(Key key, IParsable<T> value)
        where T : IParsable<T>
    {
        Add(key, (object)value);
    }

    public void Add(Key key, bool value) => Add(key, (object)value);
    public void Add(Key key, ClientId value) => Add(key, (object)value);
    public void Add(Key key, Email value) => Add(key, (object)value);
    public void Add(Key key, Enum value) => Add(key, (object)value);
    public void Add(Key key, Phone value) => Add(key, (object)value);
    public void Add(Key key, Quantity value) => Add(key, (object)value);
    public void Add(Key key, ShortId value) => Add(key, (object)value);
    public void Add(Key key, string value) => Add(key, (object)value);
    public void Add(Key key, Token value) => Add(key, (object)value);
    public void Add(Key key, Uri value) => Add(key, (object)value);

    public T Get<T>(Key key) => dict[key].Value.GetAs<T>();
    
    internal void Add(Key key, Arg value) => dict.Add(key, value);

    private void Add(Key key, object value)
    {
        key.MayNot().BeDefault();
        value.Must().Be(v => v.IsArgType());

        dict.Add(key, new Arg(value));
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<Key, Arg>> GetEnumerator() =>
        dict.GetEnumerator();
}