using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System.Net.Mail;
using WebProject.Areas.Account.Data.Interfaces;
using WebProject.Data.Interfaces;

namespace WebProject.Data.Services
{
    public class EmailServiceLocalTest : IEmailService
    {
        public async Task<bool> SendEmailAsync(string sendTo, string subjectInput, string messageInput)
        {
            var message = new MailMessage
            {
                From = new MailAddress("No-Replay@WebWiser.uk"),
                Subject = subjectInput,
                Body = messageInput,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(sendTo));

            using (var client = new SmtpClient("localhost", 25))
            {
                client.Send(message);

                return true;
            }


        }
    }
}
