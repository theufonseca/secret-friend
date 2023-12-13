using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Infra.Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration configuration;

        public NotificationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task SendConfirmationLinkAsync(long phoneNumber, string confirmCode)
        {
            string? myNumber = configuration.GetSection("Twillio:MyPhoneNumber").Value;
            string? accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string? authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            if (string.IsNullOrEmpty(accountSid))
                throw new Exception("Account Not Found");

            if (string.IsNullOrEmpty(authToken))
                throw new Exception("Token Not Found");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: $"Seu código de confirmação é: {confirmCode}",
                from: new Twilio.Types.PhoneNumber(myNumber),
                to: new Twilio.Types.PhoneNumber($"+55{phoneNumber}")
            );

            Console.WriteLine(message.Sid);
            return Task.CompletedTask;
        }
    }
}
