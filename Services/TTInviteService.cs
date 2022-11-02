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
    public class TTInviteService : ITTInviteService
    {
        private readonly ApplicationDbContext _context;

        public TTInviteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AcceptInviteAsync(Guid? token, string userId, int companyId)
        {
            try
            {
                Invite invite = await _context.Invites.FirstOrDefaultAsync(i => i.CompanyToken == token);
                if (invite == null)
                {
                    return false;
                }
                try
                {
                    invite.IsValid = false;
                    invite.InviteeId = userId;
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddNewInviteAsync(Invite invite)
        {
            try
            {
                await _context.AddAsync(invite);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AnyInviteAsync(Guid token, string email, int companyId)
        {
            try
            {
                bool result = await _context.Invites.Where(i => i.CompanyId == companyId)
                                             .AnyAsync(i => i.CompanyToken == token && i.InviteeEmail == email);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Invite> GetInviteAsync(int inviteId, int companyId)
        {
            try
            {
                Invite invite = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                       .Include(i => i.Company)
                                                       .Include(i => i.Project)
                                                       .Include(i => i.Inviter)
                                                       .FirstOrDefaultAsync(i => i.Id == inviteId);
                return invite;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Invite> GetInviteAsync(Guid token, string email, int companyId)
        {
            try
            {
                Invite invite = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                       .Include(i => i.Company)
                                                       .Include(i => i.Project)
                                                       .Include(i => i.Inviter)
                                                       .FirstOrDefaultAsync(i => i.CompanyToken == token && i.InviteeEmail == email);
                return invite!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidateInviteCodeAsync(Guid? token)
        {
            if (token == null)
            {
                return false;
            }
            bool result = false;
            Invite invite = await _context.Invites.FirstOrDefaultAsync(i => i.CompanyToken == token);
            if (invite != null)
            {
                // Determine invite date
                DateTime inviteDate = invite.InviteDate.DateTime;
                // Validation of invite based on the date it was issued and valid for 7 days
                bool validDate = (DateTime.UtcNow - inviteDate).TotalDays <= 7;
                if (validDate)
                {
                    result = invite.IsValid;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}