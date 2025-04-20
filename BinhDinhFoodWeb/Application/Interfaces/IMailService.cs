using BinhDinhFood.Presentation.Models.Mail;

namespace BinhDinhFood.Application.Interfaces;

public interface IMailService
{
    Task<bool> SendEmailAsync(MailRequest mailRequest);
}