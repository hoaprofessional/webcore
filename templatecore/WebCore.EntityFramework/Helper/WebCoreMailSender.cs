using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.EntityFramework.Helper
{
    public class WebCoreMailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                SmtpClient client = new SmtpClient("mysmtpserver");
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("username", "password");
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("whoever@me.com");
                mailMessage.To.Add(email);
                mailMessage.Body = htmlMessage;
                mailMessage.Subject = subject;
                await client.SendMailAsync(mailMessage);
            }
            catch
            {
            }
        }
    }
}
