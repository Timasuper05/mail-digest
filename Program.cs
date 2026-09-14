using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
string mailLog = Environment.GetEnvironmentVariable("MAIL_LOGIN");
string mailPas = Environment.GetEnvironmentVariable("MAIL_PASSWORD");
string tgToken = Environment.GetEnvironmentVariable("tg_token");
string tgChatId = Environment.GetEnvironmentVariable("tg_chat_id");
static async Task SendMessage(string tgToken, string tgChatId, string message)
{
    using HttpClient clientHttp = new HttpClient();
    clientHttp.BaseAddress = new Uri("https://api.telegram.org/bot" + tgToken + "/");
    var sc = await clientHttp.PostAsync("sendMessage", new StringContent(JsonSerializer.Serialize(new { chat_id = tgChatId, text = message }), System.Text.Encoding.UTF8, "application/json"));
    try
    {
       Console.WriteLine(sc.StatusCode);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        throw;
    }
   

}

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
string message = "";
if(s.Count== 0)
{
    Console.WriteLine("писем нет");
   await SendMessage(tgToken, tgChatId, "Писем нет");
    return;
}
foreach (var item in  s)
{
        Console.WriteLine($"Входящие: {item.Envelope.From} {item.Envelope.Subject} {item.Date} ");
        message += $"Входящие: {item.Envelope.From} {item.Envelope.Subject} \n";
}
await SendMessage(tgToken, tgChatId,  message);