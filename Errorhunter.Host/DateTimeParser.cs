namespace Errorhunter.Host;

public static class DateTimeParser
{
    public static DateTime FromUnix(long unixTimeStamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dateTime.AddSeconds( unixTimeStamp );
    }
}