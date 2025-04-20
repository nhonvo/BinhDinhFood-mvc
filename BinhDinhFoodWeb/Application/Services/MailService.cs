using System.Net;
using System.Net.Mail;
using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain;
using BinhDinhFood.Presentation.Models.Mail;
namespace BinhDinhFoodWeb.Application.Services;

public class MailService(AppSettings appSettings, ILogger<MailService> logger) : IMailService
{
    private readonly MailSettings _mailSettings = appSettings.MailSettings;
    private readonly ILogger<MailService> _logger = logger;
    public async Task<bool> SendEmailAsync(MailRequest request)
    {
        try
        {
            string fromMail = _mailSettings.FromMail;
            string fromPassword = _mailSettings.Password;

            MailMessage message = new()
            {
                From = new MailAddress(fromMail),
                Subject = request.Subject,
            };
            message.To.Add(new MailAddress(request.ToEmail));
            message.Body = "<html><body>" + request.Body + "</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_mailSettings.Host)
            {
                Port = _mailSettings.Port,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(message);
            _logger.LogInformation("Success to send mail to {Email}", request.ToEmail);
            return true; // Success
        }
        catch (Exception ex)
        {
            _logger.LogError("Fail to send mail to {Email}: {Exception}", request.ToEmail, ex);
            return false; // Failure
        }
    }
}

