using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Models;
// ADD #6 Services / CompanyInfo Service (part 1)
namespace TheTracker.Services.Interfaces
{
    public interface ITTCompanyInfoService
    {
        public Task<Company> GetCompanyInfoByIdAsync(int? company);
        public Task<List<TTUser>> GetAllMembersAsync(int company);
        public Task<List<Project>> GetAllProjectsAsync(int company);
        public Task<List<Ticket>> GetAllTicketsAsync(int company);
    }
}
