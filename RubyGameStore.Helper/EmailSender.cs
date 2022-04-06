using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace RubyGameStore.Helper
{
    public class EmailSender : IEmailSender
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            Email = configuration.GetValue<string>("EmailSender:Email");
            Password = configuration.GetValue<string>("EmailSender:Password");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Email, Password)
            };
            using (var message = new MailMessage(Email, email)
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
            return Task.CompletedTask;
        }
    }
}
