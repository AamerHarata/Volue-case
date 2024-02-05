namespace Volue_case.Services;

public static class FormattingService
{
    public static string ToDateFormat(this DateTime dateTime) => dateTime.ToString("dd.MM.yyyy");
    public static string ToTimeFormat(this DateTime dateTime) => dateTime.ToString("hh:mm");

    public static string ToNumberFormat(this decimal number) => $"{(int)Math.Ceiling(number)}";
}