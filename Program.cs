using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System.Net.Http.Headers;
using System.Text.Json;
string mailLog = Environment.GetEnvironmentVariable("MAIL_LOGIN");
string mailPas = Environment.GetEnvironmentVariable("MAIL_PASSWORD");
using var client = new ImapClient();
await client.ConnectAsync("imap.yandex.ru", 993, MailKit.Security.SecureSocketOptions.Auto);
if (client.IsConnected)
{
    await client.AuthenticateAsync(mailLog, mailPas);
}
DateTime timeZoneMoscow = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
client.Inbox.Open(FolderAccess.ReadOnly);
DateTime date = timeZoneMoscow.AddDays(-1);
var a = client.Inbox.Search(SearchQuery.DeliveredAfter(date));
var s = client.Inbox.Fetch(a, MessageSummaryItems.Envelope);

foreach (var item in  s)
{
    
        Console.WriteLine($"Входящие: {item.Envelope.From} {item.Envelope.Subject} {item.Date} ");
}