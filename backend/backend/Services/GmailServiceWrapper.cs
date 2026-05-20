using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;

public interface IMailService
{
    Task SendLockerNotificationAsync(string recipientEmail, string bookTitle, string lockerAddress, string openCode, int daysToPickUp);
}

public class GmailServiceWrapper : IMailService
{
    private readonly string _secretsPath = Path.Combine(AppContext.BaseDirectory, "client_secrets.json");
    private readonly string _tokenDataStorePath = Path.Combine(AppContext.BaseDirectory, "GmailTokens");

    private async Task<GmailService> GetGmailServiceAsync()
    {
        UserCredential credential;

        using (var stream = new FileStream(_secretsPath, FileMode.Open, FileAccess.Read))
        {
            // Naudojame GmailSend skopą, kad galėtume tik siųsti laiškus
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                new[] { GmailService.Scope.GmailSend },
                "user", // Vartotojo identifikatorius duomenų saugykloje
                CancellationToken.None,
                new FileDataStore(_tokenDataStorePath, true)
            );
        }

        // Sukuriame Gmail API servisą
        return new GmailService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "LibraryNotificationSystem"
        });
    }

    public async Task SendLockerNotificationAsync(string recipientEmail, string bookTitle, string lockerAddress, string openCode, int daysToPickUp)
    {
        var gmailService = await GetGmailServiceAsync();

        // 1. Sukuriame gražų HTML laišką naudojant MimeKit
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Biblioteka", "djgytis231@gmail.com"));
        message.To.Add(new MailboxAddress("", recipientEmail));
        message.Subject = $"Jūsų knyga \"{bookTitle}\" jau paštomate!";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;">
                <h2 style="color: #2c3e50;">Sveiki!</h2>
                <p>Knyga sėkmingai įdėta į paštomatą. Štai informacija atsiėmimui:</p>
                <table style="width: 100%; border-collapse: collapse; margin: 20px 0;">
                    <tr>
                        <td style="padding: 10px; font-weight: bold; background-color: #f8f9fa; border-bottom: 1px solid #e9ecef;">Knyga:</td>
                        <td style="padding: 10px; border-bottom: 1px solid #e9ecef;"><i>{bookTitle}</i></td>
                    </tr>
                    <tr>
                        <td style="padding: 10px; font-weight: bold; background-color: #f8f9fa; border-bottom: 1px solid #e9ecef;">Paštomato adresas:</td>
                        <td style="padding: 10px; border-bottom: 1px solid #e9ecef;">{lockerAddress}</td>
                    </tr>
                    <tr>
                        <td style="padding: 10px; font-weight: bold; background-color: #f8f9fa; border-bottom: 1px solid #e9ecef;">Atidarymo kodas:</td>
                        <td style="padding: 10px; border-bottom: 1px solid #e9ecef; font-size: 18px; color: #e74c3c; font-weight: bold;">{openCode}</td>
                    </tr>
                    <tr>
                        <td style="padding: 10px; font-weight: bold; background-color: #f8f9fa; border-bottom: 1px solid #e9ecef;">Terminas:</td>
                        <td style="padding: 10px; border-bottom: 1px solid #e9ecef; color: #27ae60; font-weight: bold;">{daysToPickUp} dienos (iki {DateTime.Now.AddDays(daysToPickUp):yyyy-MM-dd})</td>
                    </tr>
                </table>
                <p style="color: #7f8c8d; font-size: 12px; margin-top: 30px;">Tai automatinis pranešimas, į jį atsakinėti nereikia.</p>
            </div>
            """
        };

        message.Body = bodyBuilder.ToMessageBody();

        // 2. Koduojame MimeMessage į Base64Url formatą (ko reikalauja Gmail API)
        using var memoryStream = new MemoryStream();
        await message.WriteToAsync(memoryStream);
        var rawMessage = Convert.ToBase64String(memoryStream.ToArray())
            .Replace('+', '-')
            .Replace('/', '_')
            .Replace("=", "");

        var gmailMessage = new Message { Raw = rawMessage };

        // 3. Siunčiame per Gmail API
        await gmailService.Users.Messages.Send(gmailMessage, "me").ExecuteAsync();
    }
}