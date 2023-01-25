namespace Backend.Infrastructure.Constants;

public static class StorageKeys
{
    public static class Public
    { 
    
    }

    public static class Secure
    {
        /// <summary>
        /// stores a JWTokenModel object 
        /// </summary>
        public const string JWT = "jwt";
    }
}
