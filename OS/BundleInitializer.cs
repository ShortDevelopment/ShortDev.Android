using Android.OS;
using Android.Util;
using Java.Lang;
using System.Collections;

namespace ShortDev.Android.OS;

/// <summary>
/// Build a <see cref="Bundle"/> using a collection initializer.
/// </summary>
/// <example>
/// <code>
/// Bundle bundle = new BundleInitializer()
/// {
///     { "key1", "value1" },
///     { "key2", 42 },
///     { "key3", true }
/// }
/// </code>
/// </example>
readonly struct BundleInitializer() : IEnumerable<KeyValuePair<string, Java.Lang.Object?>>
{
    readonly Bundle _bundle = new();

    public void Add(Bundle value) => _bundle.PutAll(value);
    public void Add(string key, IBinder? value) => _bundle.PutBinder(key, value);
    public void Add(string key, bool value) => _bundle.PutBoolean(key, value);
    public void Add(string key, bool[]? value) => _bundle.PutBooleanArray(key, value);
    public void Add(string key, Bundle? value) => _bundle.PutBundle(key, value);
    public void Add(string key, sbyte value) => _bundle.PutByte(key, value);
    public void Add(string key, byte[]? value) => _bundle.PutByteArray(key, value);
    public void Add(string key, char value) => _bundle.PutChar(key, value);
    public void Add(string key, char[]? value) => _bundle.PutCharArray(key, value);
    public void Add(string key, ICharSequence[]? value) => _bundle.PutCharSequenceArray(key, value);
    public void Add(string key, IList<ICharSequence>? value) => _bundle.PutCharSequenceArrayList(key, value);
    public void Add(string key, double value) => _bundle.PutDouble(key, value);
    public void Add(string key, double[]? value) => _bundle.PutDoubleArray(key, value);
    public void Add(string key, float value) => _bundle.PutFloat(key, value);
    public void Add(string key, float[]? value) => _bundle.PutFloatArray(key, value);
    public void Add(string key, int value) => _bundle.PutInt(key, value);
    public void Add(string key, int[]? value) => _bundle.PutIntArray(key, value);
    public void Add(string key, IList<Integer>? value) => _bundle.PutIntegerArrayList(key, value);
    public void Add(string key, long value) => _bundle.PutLong(key, value);
    public void Add(string key, long[]? value) => _bundle.PutLongArray(key, value);
    public void Add(string key, IParcelable? value) => _bundle.PutParcelable(key, value);
    public void Add(string key, IParcelable[]? value) => _bundle.PutParcelableArray(key, value);
    public void Add(string key, IList<IParcelable>? value) => _bundle.PutParcelableArrayList(key, value);
    public void Add(string key, Java.IO.ISerializable? value) => _bundle.PutSerializable(key, value);
    public void Add(string key, short value) => _bundle.PutShort(key, value);
    public void Add(string key, short[]? value) => _bundle.PutShortArray(key, value);
    public void Add(string key, Size? value) => _bundle.PutSize(key, value);
    public void Add(string key, SizeF? value) => _bundle.PutSizeF(key, value);
    public void Add(string key, string? value) => _bundle.PutString(key, value);
    public void Add(string key, string[]? value) => _bundle.PutStringArray(key, value);
    public void Add(string key, IList<string>? value) => _bundle.PutStringArrayList(key, value);

    public IEnumerator<KeyValuePair<string, Java.Lang.Object?>> GetEnumerator()
    {
        foreach (var key in _bundle.KeySet() ?? [])
            yield return KeyValuePair.Create(key, _bundle.Get(key));
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static implicit operator Bundle(BundleInitializer builder) => builder._bundle;
}
