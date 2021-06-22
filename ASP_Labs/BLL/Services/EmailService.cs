using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;
using WebApp.Web.Startup.Settings;

namespace WebApp.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;
        public EmailService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_appSettings.EmailSettings.DefaultName, _appSettings.EmailSettings.DefaultEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_appSettings.EmailSettings.DefaultSMTPServer, _appSettings.EmailSettings.DefaultSMTPServerPort, true);
                await client.AuthenticateAsync(_appSettings.EmailSettings.DefaultEmail, _appSettings.EmailSettings.DefaultPassword);

                try
                {
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch
                {
                    await client.DisconnectAsync(true);
                    return false;
                }
            }

        }
    }
}
