using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Notification.Services
{
    public class NotificationService : INotificationService
    {
        public Task SendConfirmationLinkAsync(long phoneNumber, string confirmCode)
        {
            throw new NotImplementedException();
        }
    }
}
