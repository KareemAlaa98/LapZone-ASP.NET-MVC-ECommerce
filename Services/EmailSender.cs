using E_Commerce_GP.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace E_Commerce_GP.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var fromMail = _configuration["EmailSettings:Email"];
            var fromPassword = _configuration["EmailSettings:Password"];

            var message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromMail, fromPassword)
            };

           smtpClient.Send(message);
        }

    }
}
