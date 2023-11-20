namespace Mardev.Arq.Shared.Tools
{
    public static class StringExtensions
    {
        private static string Repeat(this string key, int length)
        {
            while (key.Length < length)
            {
                key += key;
            }
            return key[..length];
        }
    }
}
