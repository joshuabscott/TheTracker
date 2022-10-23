using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Data;
using TheTracker.Models;
using TheTracker.Services.Interfaces;
// ADD #11 Services / Project Service (part 1)
namespace TheTracker.Services
{
    public class TTProjectService : ITTProjectService  // Implement creates a baseline of scaffolded code to start with
    {
        private readonly ApplicationDbContext _context; /*private property*/
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<TTUser> _userManager;

        public TTProjectService(ApplicationDbContext context,
                               RoleManager<IdentityRole> roleManager,
                               UserManager<TTUser> userManager) /*method constructor*/
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // ADD #11 Services / Project Service (part 1) C.R.U.D. - CREATE
        public async Task AddNewProjectAsync(Project project)
        {
            // ADD #11 Services / Project Service (part 1)
            _context.Add(project);
            await _context.SaveChangesAsync();
        }

        public Task<bool> AddProjectManagerAsync(string userId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserToProjectAsync(string userId, int projectId)
        {
            throw new NotImplementedException();
        }


        // ADD #11 Services / Project Service (part 1) C.R.U.D. - "DELETE"
        public async Task ArchiveProjectAsync(Project project)
        {   // ADD #11 Services / Project Service (part 1)
            project.Archived = true;
            _context.Update(project);
            await _context.SaveChangesAsync();
        }

        public Task<List<TTUser>> GetAllProjectMembersExceptPMAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        // ADD #12 Services / Project Service (part 2)
        public async Task<List<Project>> GetAllProjectsByCompany(int companyId)
        {   // ADD #12 Services / Project Service (part 2)
            List<Project> projects = new();
            projects = await _context.Projects.Where(p => p.CompanyId == companyId)
                                            .Include(p => p.Members)

                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Comments)

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
            return projects;
        }

        // ADD #12 Services / Project Service (part 2)
        public async Task<List<Project>> GetAllProjectsByPriority(int companyId, string priorityName)
        {   // ADD #12 Services / Project Service (part 2)
            List<Project> projects = await GetAllProjectsByCompany(companyId);
            int priorityId = await LookupProjectPriorityId(priorityName);
            return projects.Where(p => p.ProjectPriorityId == priorityId).ToList();
        }

        // ADD #12 Services / Project Service (part 2)
        public async Task<List<Project>> GetArchivedProjectsByCompany(int companyId)
        {   // ADD #12 Services / Project Service (part 2)
            List<Project> projects = await GetAllProjectsByCompany(companyId);
            return projects.Where(p => p.Archived == true).ToList();
        }

        
        public Task<List<TTUser>> GetDevelopersOnProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        // ADD #11 Services / Project Service (part 1) C.R.U.D. - READ
        public async Task<Project> GetProjectByIdAsync(int projectId, int companyId)
        {   // ADD #11 Services / Project Service (part 1)
            Project project = await _context.Projects
                                            .Include(p => p.Tickets)
                                            .Include(p => p.Members)
                                            .Include(p => p.ProjectPriority)
                                            .FirstOrDefaultAsync(p =>p.Id == projectId && p.CompanyId == companyId);
            return project;
        }

        
        public Task<TTUser> GetProjectManagerAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TTUser>> GetProjectMembersByRoleAsync(int projectId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<List<TTUser>> GetSubmittersOnProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetUserProjectsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TTUser>> GetUsersNotOnProjectAsync(int projectId, int companyId)
        {
            throw new NotImplementedException();
        }

       
        public Task<bool> IsUserOnProject(string userId, int projectId)
        {
            throw new NotImplementedException();
        }

        // ADD #12 Services / Project Service (part 2)
        public async Task<int> LookupProjectPriorityId(string priorityName)
        {   // ADD #12 Services / Project Service (part 2)
            int priorityId = (await _context.ProjectPriorities.FirstOrDefaultAsync(p => p.Name == priorityName)).Id;
            return priorityId;
        }

       
        public Task RemoveProjectManagerAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserFromProjectAsync(string userId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUsersFromProjectByRoleAsync(string role, int projectId)
        {
            throw new NotImplementedException();
        }

        // ADD #11 Services / Project Service (part 1) C.R.U.D. - UPDATE
        public async Task UpdateProjectAsync(Project project)
        {   // ADD #11 Services / Project Service (part 1)
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
