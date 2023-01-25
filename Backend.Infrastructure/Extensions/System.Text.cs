namespace System.Text
{
    public static class SystemExtensions
    {
        public static string Reverse(this string text)
        {
            if (text == null) return null;
            char[] charArray = text.ToCharArray();
            int len = text.Length - 1;

            for (int i = 0; i < len; i++, len--)
            {
                charArray[i] ^= charArray[len];
                charArray[len] ^= charArray[i];
                charArray[i] ^= charArray[len];
            }

            return new string(charArray);
        }
    }
}
