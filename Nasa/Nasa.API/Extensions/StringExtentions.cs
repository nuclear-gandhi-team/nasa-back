
namespace Nasa.API.Extentions
{
    public static class StringExtentions
    {
        public static string FirstCharacterToUpper(this string str)
        {
            if (str.Length <= 1)
                return str.ToUpper();

            return str.ToUpper()[0].ToString() + str[1..];
        }
    }
}
