using System.Text.RegularExpressions;
/// <summary>
/// Summary description for UtilsRegex
/// </summary>
public static class UtilsRegex
{
    private static Regex Phone = new Regex(@"^[0-9]+$",
                                              RegexOptions.IgnoreCase);
 
    /// </summary>
    public static bool CheckPhone(string str)
    {
        return Phone.IsMatch(str);
    }

}