using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RealState.Core.Application.Dtos.Email;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Domain.Settings;
using System;
using System.Threading.Tasks;

namespace RealState.Infrastructure.Shared.Service
{
    public class EmailService : IEmailService
    {
        private readonly MainSetting _mainSetting;

        public EmailService(IOptions<MainSetting> mainSetting)
        {
            _mainSetting = mainSetting.Value ?? throw new ArgumentNullException(nameof(mainSetting));
        }

        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(MailboxAddress.Parse($"{_mainSetting.DisplayName} <{_mainSetting.EmailFrom}>"));
                message.To.Add(MailboxAddress.Parse(request.To));
                message.Subject = request.Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                message.Body = builder.ToMessageBody();

                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(_mainSetting.SmtpHost, _mainSetting.SmtpPort, SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(_mainSetting.SmtpUser, _mainSetting.SmtpPass);
                    await smtpClient.SendAsync(message);
                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending email: {ex}");
                throw; // Re-throwing the exception to handle it at the caller's level
            }
        }
    }
}
