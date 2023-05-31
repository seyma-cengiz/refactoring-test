namespace LegacyApp.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEmailValid(this string email)
        {
            return email.Contains("@") && !email.Contains(".");
        }
    }
}
