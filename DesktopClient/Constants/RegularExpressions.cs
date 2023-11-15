using System.Text.RegularExpressions;

namespace DesktopClient.Constants;

public static class RegularExpressions
{
    public static readonly Regex AtLeastOneDigit = new Regex(".*[0-9].*");
    public static readonly Regex AtLeastOneLetter = new Regex("^(?=[^A-Za-z]*[A-Za-z])[\x00-\x7F]*$");
    public static readonly Regex AtLeastOneUppercase = new Regex("(?=.*[A-Z])");
    public static readonly Regex AtLeastOneLowercase = new Regex("(?=.*[a-z])");
    public static readonly Regex AtLeastOneSpecialCharacter = new Regex("(?=.*[#$^+=!*()@%&])");
    public static readonly Regex PhoneNumber = new Regex("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$");
    public static readonly Regex Email = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
}