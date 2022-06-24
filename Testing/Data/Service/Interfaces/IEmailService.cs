using A5.Models;
namespace A5.DataAccessLayer.Interfaces{
    public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
}