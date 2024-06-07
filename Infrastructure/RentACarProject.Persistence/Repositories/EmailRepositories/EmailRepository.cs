using Microsoft.Extensions.Configuration;
using RentACarProject.Application.Interfaces.EmailInterfaces;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.EmailRepositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("Smtp");
                var host = smtpSettings["Host"];
                var port = int.Parse(smtpSettings["Port"]);
                var enableSsl = bool.Parse(smtpSettings["EnableSsl"]);
                var userName = smtpSettings["UserName"];
                var password = smtpSettings["Password"];

                var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(userName, password),
                    //EnableSsl = enableSsl
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(userName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Hata loglama veya işleme
                Console.WriteLine($"Email gönderme hatası: {ex.Message}");
                throw; // Hatanın yukarıya fırlatılması (opsiyonel)
            }
        }
    }
}
