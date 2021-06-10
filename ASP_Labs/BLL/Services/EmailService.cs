using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;

namespace WebApp.BLL.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("MaybeBabyFromTheBlood", "Igritt33@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                /*await client.ConnectAsync("aspmx.l.google.com", 25, false);*/
                await client.AuthenticateAsync("Igritt33@gmail.com", "Igritt2002");

                try
                {
                    await client.SendAsync(emailMessage);
                    return true;
                }
                catch
                {

                }
                finally
                {
                    await client.DisconnectAsync(true);
                }

                return false;
            }
        }
    }
}
