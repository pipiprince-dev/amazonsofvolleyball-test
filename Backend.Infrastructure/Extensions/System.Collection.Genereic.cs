namespace System.Collections.Generic;

public static class DictionaryExtensions
{
       public static V GetValueOrDefault<K, V>(this Dictionary<K, V> dic, K key)
       {
        bool found = dic.TryGetValue(key, out V result);
        if (found)
            {
                return result;
            }
            return default(V);
        }
}
