using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Models;
// ADD #24 Services / Email Service
namespace TheTracker.Services
{
    public class TTEmailService : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public TTEmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
        {
            var emailSender = _mailSettings.Mail ?? Environment.GetEnvironmentVariable("EmailAddress");
            var host = _mailSettings.Host ?? Environment.GetEnvironmentVariable("EmailHost");
            var port = _mailSettings.Port != 0 ? _mailSettings.Port : int.Parse(Environment.GetEnvironmentVariable("EmailPort")!);
            string password = _mailSettings.Password ?? Environment.GetEnvironmentVariable("EmailPassword")!;

            MimeMessage newEmail = new();

            //Add all email address to the "TO" for the email.
            newEmail.Sender = MailboxAddress.Parse(emailSender);
            newEmail.To.Add(MailboxAddress.Parse(emailTo));
            newEmail.Subject = subject;

            //Add the body to the email.
            var builder = new BodyBuilder { HtmlBody = htmlMessage };
            newEmail.Body = builder.ToMessageBody();

            //Send the email.
            try
            {
                using SmtpClient smtpClient = new();

                await smtpClient.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(emailSender, password);
                await smtpClient.SendAsync(newEmail);

                await smtpClient.DisconnectAsync(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
