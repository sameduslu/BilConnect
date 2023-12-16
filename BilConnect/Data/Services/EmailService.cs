using SendGrid.Helpers.Mail;
using SendGrid;

namespace BilConnect.Data.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            var apiKey = _configuration["EmailSettings:SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("emre.akgul@ug.bilkent.edu.tr", "bilconnect-app-apikey");
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, "", htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
