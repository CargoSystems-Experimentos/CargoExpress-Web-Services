using System.Text.RegularExpressions;

namespace ACME.CargoExpress.API.Shared.Interfaces.ASP.Configuration.Extensions;

public static class InputValidationExtensions
{
    public static bool IsValidEmail(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        return Regex.IsMatch(value.Trim(), @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
    }

    public static bool IsValidPassword(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        return value.Length >= 8
               && value.Any(char.IsUpper)
               && value.Any(char.IsDigit)
               && value.Any(c => !char.IsLetterOrDigit(c));
    }

    public static bool IsDigitsWithLength(this string? value, int length)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        return value.Length == length && value.All(char.IsDigit);
    }
}
