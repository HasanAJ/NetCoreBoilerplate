using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NetCoreBoilerplate.Application.Common.Config;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Models;

namespace NetCoreBoilerplate.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpConfig _smtConfig;

        public MailService(IOptions<SmtpConfig> smtpConfig)
        {
            _smtConfig = smtpConfig.Value;
        }

        public async Task Send(Email email)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(email.From ?? _smtConfig.User));
            message.To.Add(MailboxAddress.Parse(email.To));
            message.Subject = email.Subject;
            message.Body = new TextPart(TextFormat.Html) { Text = email.Body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtConfig.Host, _smtConfig.Port, SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(_smtConfig.User, _smtConfig.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        public async Task Send<T>(Email<T> email) where T : class
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(email.From ?? _smtConfig.User));
            message.To.Add(MailboxAddress.Parse(email.To));
            message.Subject = email.Subject;
            message.Body = new TextPart(TextFormat.Html) { Text = ParseTemplate(email.Template, email.Model) };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtConfig.Host, _smtConfig.Port, SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(_smtConfig.User, _smtConfig.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        private string ParseTemplate<T>(string template, T model)
        {
            string body = File.ReadAllText($"{Directory.GetCurrentDirectory()}/EmailTemplates/{template}");

            if (model != null)
            {
                foreach (PropertyInfo propertyInfo in model.GetType().GetRuntimeProperties())
                {
                    body = body.Replace($"##{propertyInfo.Name}##", propertyInfo.GetValue(model, null).ToString());
                }
            }

            return body;
        }
    }
}
