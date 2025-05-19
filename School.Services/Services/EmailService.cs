using MailKit.Net.Smtp;
using MimeKit;
using School.Data.Helpers.Email;
using School.Services.ServicesContracts;

namespace School.Services.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        public async Task<bool> SendEmail(string email, string Message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("SchoohAPI", _emailSettings.SenderEmail));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = "new Contact Submitted Data";
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
