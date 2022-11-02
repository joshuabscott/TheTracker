using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Data;
using TheTracker.Models;
using TheTracker.Services.Interfaces;

namespace TheTracker.Services
{
    public class TTNotificationService : ITTNotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailService;
        private readonly ITTRolesService _rolesService;

        public TTNotificationService(ApplicationDbContext context,
                                     IEmailSender emailService,
                                     ITTRolesService rolesService)
        {
            _context = context;
            _emailService = emailService;
            _rolesService = rolesService;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            try
            {
                await _context.AddAsync(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Notification>> GetRecivedNotificationsAsync(string userId)
        {
            try
            {
                List<Notification> notifications = await _context.Notifications
                                                         .Include(n => n.Recipient)
                                                         .Include(n => n.Sender)
                                                         .Include(n => n.Ticket)
                                                         .ThenInclude(t => t.Project)
                                                         .Where(n => n.RecipientId == userId).ToListAsync();
                return notifications;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Notification>> GetSentNotificationsAsync(string userId)
        {
            try
            {
                List<Notification> notifications = await _context.Notifications
                                                         .Include(n => n.Recipient)
                                                         .Include(n => n.Sender)
                                                         .Include(n => n.Ticket)
                                                         .ThenInclude(t => t.Project)
                                                         .Where(n => n.SenderId == userId).ToListAsync();
                return notifications;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SendEmailNotificationAsync(Notification notification, string emailSubject)
        {
            TTUser ttUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == notification.RecipientId);
            if(ttUser != null)
            {
                string ttUserEmail = ttUser.Email;
                string message = notification.Message;
                //Send Email
                try
                {
                    await _emailService.SendEmailAsync(ttUserEmail, emailSubject, message);
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task SendEmailNotificationByRoleAsync(Notification notification, int companyId, string role)
        {
            try
            {
                List<TTUser> members = await _rolesService.GetUsersInRoleAsync(role, companyId);
                foreach (TTUser ttUser in members)
                {
                    notification.RecipientId = ttUser.Id;
                    await SendEmailNotificationAsync(notification, notification.Title);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendMembersEmailNotificationsAsync(Notification notification, List<TTUser> members)
        {
            try
            {
                foreach (TTUser ttUser in members)
                {
                    notification.RecipientId = ttUser.Id;
                    await SendEmailNotificationAsync(notification, notification.Title);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}