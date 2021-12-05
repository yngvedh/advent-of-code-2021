namespace AoC.String
{
    public static class StringExtensions
    {
        public static string Capitalize(this string s) => s switch
        {
            "" => "",
            _ => s.Substring(0, 1).ToUpper() + s.Substring(1)
        };
    }
}