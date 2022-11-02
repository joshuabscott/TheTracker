using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Models;

namespace TheTracker.Services.Interfaces
{
    public interface ITTNotificationService
    {
        public Task AddNotificationAsync(Notification notification);
       
        public Task<List<Notification>> GetRecivedNotificationsAsync(string userId);

        public Task<List<Notification>> GetSentNotificationsAsync(string userId);

        public Task SendEmailNotificationByRoleAsync(Notification notification, int companyId, string role);

        public Task SendMembersEmailNotificationsAsync(Notification notification, List<TTUser> members);

        public Task<bool> SendEmailNotificationAsync(Notification notification, string emailSubject);
    }
}