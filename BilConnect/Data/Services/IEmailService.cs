namespace BilConnect.Data.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlContent);
    }
}
