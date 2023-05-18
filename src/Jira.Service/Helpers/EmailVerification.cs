﻿using Jira.Service.Exceptions;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using StackExchange.Redis;

namespace Jira.Service.Helpers
{
    public class EmailVerification
    {
        private readonly IConfiguration configuration;
        public EmailVerification(IConfiguration configuration)
        {
            this.configuration = configuration.GetSection("Email");
        }

        public async Task<string> SendAsync(string to)
        {
            try
            {
                Random random = new Random();
                int verificationCode = random.Next(123456, 999999);

                ConnectionMultiplexer resdisConnect = ConnectionMultiplexer.Connect("localhost");
                IDatabase db = resdisConnect.GetDatabase();
                db.StringSet("code", verificationCode.ToString());
                var result = db.StringGet("code");

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(this.configuration["EmailAddress"]));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = "Email verification Ahsan.uz";
                email.Body = new TextPart(TextFormat.Html) { Text = verificationCode.ToString() };

                var sendMessage = new MailKit.Net.Smtp.SmtpClient();
                await sendMessage.ConnectAsync(this.configuration["Host"], 587, SecureSocketOptions.StartTls);
                await sendMessage.AuthenticateAsync(this.configuration["EmailAddress"], this.configuration["Password"]);
                await sendMessage.SendAsync(email);
                await sendMessage.DisconnectAsync(true);

                return verificationCode.ToString();
            }
            catch (Exception ex)
            {
                throw new JiraException(400, ex.Message);
            }
        }
    }
}
