namespace ismailaktasblog.Models
{
    public interface IMailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
