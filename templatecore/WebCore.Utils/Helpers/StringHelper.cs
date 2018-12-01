namespace WebCore.Utils.Helpers
{
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return s == null || s.Trim().Equals(string.Empty);
        }
    }
}
