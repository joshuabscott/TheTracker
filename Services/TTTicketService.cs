using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Data;
using TheTracker.Models;
using TheTracker.Services.Interfaces;
// ADD #16 Services / Ticket Service - Intro
namespace TheTracker.Services
{
    public class TTTicketService : ITTTicketService // Implement creates a baseline of scaffolded code to start with
    {
        private readonly ApplicationDbContext _context; /*private property*/
        private readonly ITTRolesService _rolesService;

        public TTTicketService(ApplicationDbContext context,
                               ITTRolesService rolesService) /*method constructor*/
        {
            _context = context;
            _rolesService = rolesService;
        }
        // ADD #17 Services / Ticket Service (part 1) C.R.U.D. - CREATE
        public async Task AddNewTicketAsync(Ticket ticket)
        {   //REFERENCE TicketsController Create POST
            _context.Add(ticket);
            await _context.SaveChangesAsync();
        }
        // ADD #17 Services / Ticket Service (part 1) C.R.U.D. - "DELETE"
        public async Task ArchiveTicketAsync(Ticket ticket)
        {
            ticket.Archived = true;
            _context.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public Task AssignTicketAsync(int ticketId, string userId)
        {
            throw new NotImplementedException();
        }
        // ADD #19 Services / Ticket Service (part 3)
        public async Task<List<Ticket>> GetAllTicketsByCompanyAsync(int companyId)
        {
            try
            {
                List<Ticket> tickets = await _context.Projects
                                                     .Where(p => p.CompanyId == companyId)
                                                     .SelectMany(p => p.Tickets)
                                                        .Include(t => t.Attachments)
                                                        .Include(t => t.Comments)
                                                        .Include(t => t.History)
                                                        .Include(t => t.DeveloperUser)
                                                        .Include(t => t.OwnerUser)
                                                        .Include(t => t.TicketPriority)
                                                        .Include(t => t.TicketStatus)
                                                        .Include(t => t.TicketType)
                                                        .Include(t => t.Project)
                                                    .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // ADD #19 Services / Ticket Service (part 3)
        public async Task<List<Ticket>> GetAllTicketsByPriorityAsync(int companyId, string priorityName)
        {
            int priorityId = (await LookupTicketPriorityIdAsync(priorityName)).Value;
            try
            {
                List<Ticket> tickets = await _context.Projects
                                                     .Where(p => p.CompanyId == companyId)
                                                     .SelectMany(p => p.Tickets)
                                                        .Include(t => t.Attachments)
                                                        .Include(t => t.Comments)
                                                        .Include(t => t.History)
                                                        .Include(t => t.DeveloperUser)
                                                        .Include(t => t.OwnerUser)
                                                        .Include(t => t.TicketPriority)
                                                        .Include(t => t.TicketStatus)
                                                        .Include(t => t.TicketType)
                                                        .Include(t => t.Project)
                                                    .Where(t => t.TicketPriorityId == priorityId)
                                                    .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // ADD #19 Services / Ticket Service (part 3)
        public async Task<List<Ticket>> GetAllTicketsByStatusAsync(int companyId, string statusName)
        {
            int statusId = (await LookupTicketStatusIdAsync(statusName)).Value;
            try
            {
                List<Ticket> tickets = await _context.Projects
                                                     .Where(p => p.CompanyId == companyId)
                                                     .SelectMany(p => p.Tickets)
                                                        .Include(t => t.Attachments)
                                                        .Include(t => t.Comments)
                                                        .Include(t => t.History)
                                                        .Include(t => t.DeveloperUser)
                                                        .Include(t => t.OwnerUser)
                                                        .Include(t => t.TicketPriority)
                                                        .Include(t => t.TicketStatus)
                                                        .Include(t => t.TicketType)
                                                        .Include(t => t.Project)
                                                    .Where(t => t.TicketStatusId == statusId)
                                                    .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // ADD #19 Services / Ticket Service (part 3)
        public async Task<List<Ticket>> GetAllTicketsByTypeAsync(int companyId, string typeName)
        {
            int typeId = (await LookupTicketTypeIdAsync(typeName)).Value;
            try
            {
                List<Ticket> tickets = await _context.Projects
                                                     .Where(p => p.CompanyId == companyId)
                                                     .SelectMany(p => p.Tickets)
                                                        .Include(t => t.Attachments)
                                                        .Include(t => t.Comments)
                                                        .Include(t => t.History)
                                                        .Include(t => t.DeveloperUser)
                                                        .Include(t => t.OwnerUser)
                                                        .Include(t => t.TicketPriority)
                                                        .Include(t => t.TicketStatus)
                                                        .Include(t => t.TicketType)
                                                        .Include(t => t.Project)
                                                    .Where(t => t.TicketTypeId == typeId)
                                                    .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<List<Ticket>> GetArchivedTicketsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByPriorityAsync(string priorityName, int companyId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByRoleAsync(string role, string userId, int projectId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByStatusAsync(string statusName, int companyId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByTypeAsync(string typeName, int companyId, int projectId)
        {
            throw new NotImplementedException();
        }
        // ADD #17 Services / Ticket Service (part 1) C.R.U.D. - READ
        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public Task<TTUser> GetTicketDeveloperAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetTicketsByRoleAsync(string role, string userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetTicketsByUserIdAsync(string userId, int companyId)
        {
            throw new NotImplementedException();
        }
        // ADD #18 Services / Ticket Service (part 2)
        public async Task<int?> LookupTicketPriorityIdAsync(string priorityName)
        {
            try
            {
                TicketPriority priority = await _context.TicketPriorities.FirstOrDefaultAsync(p => p.Name == priorityName);
                return priority?.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // ADD #18 Services / Ticket Service (part 2)
        public async Task<int?> LookupTicketStatusIdAsync(string statusName)
        {
            try
            {
                TicketStatus status = await _context.TicketStatuses.FirstOrDefaultAsync(p => p.Name == statusName);
                return status?.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // ADD #18 Services / Ticket Service (part 2)
        public async Task<int?> LookupTicketTypeIdAsync(string typeName)
        {
            try
            {
                TicketType type = await _context.TicketTypes.FirstOrDefaultAsync(p => p.Name == typeName);
                return type?.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // ADD #17 Services / Ticket Service (part 1) C.R.U.D. - UPDATE
        public async Task UpdateTicketAsync(Ticket ticket)
        {   //REFERENCE TicketsController Create POST
            _context.Add(ticket);
            await _context.SaveChangesAsync();

        }
    }
}