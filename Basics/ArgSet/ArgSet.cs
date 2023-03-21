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
        AddKV(key, value);
    }

    public void Add(Key key, bool value) => AddKV(key, value);
    public void Add(Key key, EmailAddress value) => AddKV(key, value);
    public void Add(Key key, Enum value) => AddKV(key, value);
    public void Add(Key key, Token value) => AddKV(key, value);
    public void Add(Key key, PhoneNumber value) => AddKV(key, value);
    public void Add(Key key, Quantity value) => AddKV(key, value);
    public void Add(Key key, Ratchet value) => AddKV(key, value);
    public void Add(Key key, string value) => AddKV(key, value);
    public void Add(Key key, Uri value) => AddKV(key, value);

    public T Get<T>(Key key) => dict[key].Value.GetAs<T>();
    
    internal void Add(Key key, Arg value) => dict.Add(key, value);

    private void AddKV(Key key, object value)
    {
        key.MayNot().BeDefault();
        value.Must().Be(v => v.IsArgType());

        dict.Add(key, new Arg(value));
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<Key, Arg>> GetEnumerator() =>
        dict.GetEnumerator();
}