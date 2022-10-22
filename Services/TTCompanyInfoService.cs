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
    public class TTCompanyInfoService : ITTCompanyInfoService
    {
        private readonly ApplicationDbContext _context; /*private property*/

        public TTCompanyInfoService(ApplicationDbContext context) /*method constructor*/
        {
            _context = context;
        }

        public async Task<List<TTUser>> GetAllMembersAsync(int companyId)
        {
            // MODIFY #7 Services / CompanyInfo Service (part 2)
            //throw new NotImplementedException();
            List<TTUser> result = new List<TTUser>();
            result = await _context.Users.Where(u => u.CompanyId == companyId).ToListAsync();
            return result;
        }

        public async Task<List<Project>> GetAllProjectsAsync(int companyId)
        {
            // MODIFY #8 Services / CompanyInfoService (part 3)
            //throw new NotImplementedException();
            List<Project> result = new List<Project>();
            result = await _context.Projects.Where(p => p.CompanyId == companyId)
                                            .Include(p => p.Members)

                                            .Include(p => p.Tickets)
                                                .ThenInclude(t=>t.Comments)

                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Attachments)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.History)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Notifications)

                                             .Include(p => p.Tickets)
                                                .ThenInclude(t => t.DeveloperUser)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.OwnerUser)

                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketPriority)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketStatus)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketType)

                                            .Include(p => p.ProjectPriority)
                                            .ToListAsync();
            return result;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync(int companyId)
        {
            // MODIFY #9 Services / CompanyInfo Service (part 4)
            //throw new NotImplementedException();
            List<Ticket> result = new List<Ticket>();
            List<Project> projects = new List<Project>();

            projects = await GetAllProjectsAsync(companyId);
            result = projects.SelectMany(p => p.Tickets).ToList();
            return result;

        }

        public async Task<Company> GetCompanyInfoByIdAsync(int? companyId)
        {
            // MODIFY #9 Services / CompanyInfo Service (part 4)
            //throw new NotImplementedException();
            Company result = new();
            if (companyId != null)
            {
                result = await _context.Companies
                                        .Include(c => c.Members)
                                        .Include(c => c.Projects)
                                        .Include(c => c.Invites)
                                        .FirstOrDefaultAsync(c => c.Id == companyId);
            }
            return result;
        }
    }
}
