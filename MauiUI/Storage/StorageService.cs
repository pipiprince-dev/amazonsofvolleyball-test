namespace MauiUI.Storage;

public static class StorageService
{
    public static class Public
    {
        public static T Get<T>(string key)
        {
            var data = Preferences.Default.Get<string>(key, string.Empty);

            if(string.IsNullOrWhiteSpace(data))
                return (T)default;

            var value = JsonSerializer.Deserialize<T>(data);

            return value;
        }

        public static void Save<T>(string key, T value)
        {
            var data = JsonSerializer.Serialize(value);
            Preferences.Default.Set<string>(key, data);
        }

        public static void Remove(string key)
        {
            Preferences.Default.Remove(key);
        }

        public static void Clear()
        {
            Preferences.Default.Clear();
        }
    }


    public static class Secure
    { 
        public static async Task SaveAsync<T>(string key, T data)
        {
            var value = JsonSerializer.Serialize(data);
            await SecureStorage.Default.SetAsync(key, value);
        }

        public static async Task<T> GetAsync<T>(string key)
        {
            try
            {
                var value = await SecureStorage.Default.GetAsync(key);

                if (string.IsNullOrWhiteSpace(value))
                    return (T)default;

                var data = JsonSerializer.Deserialize<T>(value);
                return data;
            }
            catch(Exception ex)
            {
                return (T)default;
            }
        }

        public static bool Remove(string key)
        {
            return SecureStorage.Default.Remove(key);
        }

        public static void RemoveAll()
        {
            SecureStorage.Default.RemoveAll();
        }
    }
}
