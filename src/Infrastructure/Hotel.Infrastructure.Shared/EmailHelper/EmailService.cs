using Hotel.Core.Application.Interfaces.Infrastructure.Utils;
using Hotel.Core.Application.Models;
using Hotel.Core.Application.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Hotel.Infrastructure.Utils.EmailHelper
{
    public class EmailService : IEmailService
    {
        private readonly SendGridSettings _SendGridSettings;

        public EmailService(IOptions<SendGridSettings> SendGridSettings)
        {
            _SendGridSettings = SendGridSettings.Value;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_SendGridSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _SendGridSettings.FromAddress,
                Name = _SendGridSettings.FromName
            };

            var senGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var respone = await client.SendEmailAsync(senGridMessage);

            if (respone.StatusCode == System.Net.HttpStatusCode.Accepted || respone.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
