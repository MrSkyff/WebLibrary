using System.Net;
using System.Net.Mail;
using WebProject.Data.Interfaces;

namespace WebProject.Data.Services
{
    public class EmailService : IEmailService
    {


        public async Task<bool> SendEmailAsync(string sendTo, string subjectText, string messageText)
        {
            string host = "email-smtp.eu-west-2.amazonaws.com";
            int port = 587;
            string smtpUsername = Environment.GetEnvironmentVariable("AWS_USERNAME")!;
            string smtpPassword = Environment.GetEnvironmentVariable("AWS_PASSWORD")!;

            SmtpClient client = new(host, port);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;

            MailAddress fromAddress = new(Environment.GetEnvironmentVariable("AWS_MAILFROM")!, "WebWiser.uk", System.Text.Encoding.UTF8);
            MailAddress toAddress = new(sendTo, sendTo, System.Text.Encoding.UTF8);
            MailMessage msg = new(fromAddress, toAddress)
            {
                Body = messageText,
                BodyEncoding = System.Text.Encoding.UTF8,
                Subject = subjectText,
                SubjectEncoding = System.Text.Encoding.UTF8,
            };

            client.Send(msg);
            if (true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        //public async Task<bool> SendEmailAsync(string sendTo, string subjectText, string messageText)
        //{
        //    var apiKey = Environment.GetEnvironmentVariable("SEND_GRID_APIKEY");
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress(Environment.GetEnvironmentVariable("SEND_GRID_EMAIL_FROM"), Environment.GetEnvironmentVariable("SEND_GRID_EMAIL_FROM_NAME"));
        //    var subject = subjectText;
        //    var to = new EmailAddress(sendTo, sendTo);
        //    var plainTextContent = messageText;
        //    var htmlContent = messageText;
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var result = await client.SendEmailAsync(msg);

        //    if (result.StatusCode == HttpStatusCode.OK)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
    }
}
