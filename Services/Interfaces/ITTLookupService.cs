using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Models;

namespace TheTracker.Services.Interfaces
{
    public interface ITTLookupService
    {
        //Methods
        public Task<List<ProjectPriority>> GetProjectPrioritiesAsync();

        public Task<List<TicketPriority>> GetTicketPrioritiesAsync();

        public Task<List<TicketStatus>> GetTicketStatusesAsync();

        public Task<List<TicketType>> GetTicketTypesAsync();
    }
}