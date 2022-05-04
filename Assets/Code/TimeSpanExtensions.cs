using System;

public static class TimeSpanExtensions
{
    public static TimeSpan Milliseconds(this int milliseconds) => TimeSpan.FromMilliseconds(milliseconds);
    public static TimeSpan Seconds(this int seconds) => TimeSpan.FromSeconds(seconds);
    public static TimeSpan Minutes(this int minutes) => TimeSpan.FromMinutes(minutes);
    public static TimeSpan Hours(this int hours) => TimeSpan.FromHours(hours);
    public static TimeSpan Days(this int days) => TimeSpan.FromDays(days);

    public static TimeSpan Milliseconds(this float milliseconds) => TimeSpan.FromMilliseconds(milliseconds);
    public static TimeSpan Seconds(this float seconds) => TimeSpan.FromSeconds(seconds);
    public static TimeSpan Minutes(this float minutes) => TimeSpan.FromMinutes(minutes);
    public static TimeSpan Hours(this float hours) => TimeSpan.FromHours(hours);
    public static TimeSpan Days(this float days) => TimeSpan.FromDays(days);

    public static TimeSpan Milliseconds(this double milliseconds) => TimeSpan.FromMilliseconds(milliseconds);
    public static TimeSpan Seconds(this double seconds) => TimeSpan.FromSeconds(seconds);
    public static TimeSpan Minutes(this double minutes) => TimeSpan.FromMinutes(minutes);
    public static TimeSpan Hours(this double hours) => TimeSpan.FromHours(hours);
    public static TimeSpan Days(this double days) => TimeSpan.FromDays(days);
}

