using System.Net.Http.Headers;
using System.Text.Json;

Console.WriteLine("Time:");
DateTime timeZoneMoscow = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
Console.WriteLine(timeZoneMoscow);