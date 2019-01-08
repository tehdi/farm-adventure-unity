using System.Collections.Generic;

public static class ExtensionMethods
{
    // https://stackoverflow.com/a/538751/9319242
    // returns the value mapped to the given key, if it exists. else returns the default value for the value type
    public static TValue GetValueOrDefault<TKey,TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var ret) ? ret : default;
}
