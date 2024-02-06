namespace Volue_case.Services.CommonHelpers;

public static class FormattingService
{
    public static string ToDateFormat(this DateTime dateTime) => dateTime.Date.ToString("dd.MM.yyyy");
    public static string ToDateRequestFormat(this DateTime dateTime) => $"{dateTime.Date:yyyy-M-d}";
    public static string ToTimeFormat(this DateTime dateTime) => dateTime.ToString("hh:mm");

    public static string ToNumberFormat(this decimal number) => 
        number is > 0 and < 10
            ? $"0{(int)Math.Ceiling(number)}"
            : $"{(int)Math.Ceiling(number)}";

    public static string ToNumberFormat(this int number) =>
        number is > 0 and < 10
            ? $"0{number}"
            : $"{number}";


}