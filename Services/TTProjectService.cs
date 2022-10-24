using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Data;
using TheTracker.Models;
using TheTracker.Models.Enums;
using TheTracker.Services.Interfaces;
// ADD #11 Services / Project Service (part 1)
namespace TheTracker.Services
{
    public class TTProjectService : ITTProjectService  // Implement creates a baseline of scaffolded code to start with
    {
        private readonly ApplicationDbContext _context; /*private property*/
        private readonly ITTRolesService _rolesService;

        public TTProjectService(ApplicationDbContext context,
                               ITTRolesService rolesService) /*method constructor*/
        {
            _context = context;
            _rolesService = rolesService;
        }

        // ADD #11 Services / Project Service (part 1) C.R.U.D. - CREATE
        public async Task AddNewProjectAsync(Project project)
        {
            // ADD #11 Services / Project Service (part 1)
            _context.Add(project);
            await _context.SaveChangesAsync();
        }
        // ADD #15 Services / Project Service (part 5)
        public async Task<bool> AddProjectManagerAsync(string userId, int projectId)
        {   // ADD #15 Services / Project Service (part 5)
            TTUser currentPM = await GetProjectManagerAsync(projectId);
            //Remove the current PM if necessary
            if(currentPM != null)
            {
                try
                {
                    await RemoveProjectManagerAsync(projectId);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"*** ERROR, GREATSCOTT! *** - Error Removing current PM. ---> {ex.Message}");
                    return false;
                }
            }
            // ADD the new PM
            try
            {
                await AddUserToProjectAsync(userId, projectId);//change this from AddProjectManagerAsync
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"*** ERROR, GREATSCOTT! *** - Error Adding PM to project. ---> {ex.Message}");
                return false;
            }
        }
        // ADD #13 Services / Project Service (part 3)
        public async Task<bool> AddUserToProjectAsync(string userId, int projectId)
        {   // ADD #13 Services / Project Service (part 3)
            TTUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user != null)
            {
                Project project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
                if(!await IsUserOnProjectAsync(userId, projectId))
                {
                    try 
                    {
                        project.Members.Add(user);
                        await _context.SaveChangesAsync();
                        return true;    //Prevent User from being added to same project twice buy returning true or false on all paths
                    }
                    catch(Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    return false;   //Prevent User from being added to same project twice buy returning true or false on all paths
                }
            }
            else
            {
                return false;   //Prevent User from being added to same project twice buy returning true or false on all paths
            }
        }
        // ADD #11 Services / Project Service (part 1) C.R.U.D. - "DELETE"
        public async Task ArchiveProjectAsync(Project project)
        {   // ADD #11 Services / Project Service (part 1)
            project.Archived = true;
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
        // ADD #14 Services / Project Service (part 4)
        public async Task<List<TTUser>> GetAllProjectMembersExceptPMAsync(int projectId)
        {   // ADD #14 Services / Project Service (part 4)
            List<TTUser> admins = await GetProjectMembersByRoleAsync(projectId, Roles.Admin.ToString());
            List<TTUser> submitters = await GetProjectMembersByRoleAsync(projectId, Roles.Developer.ToString());
            List<TTUser> developers = await GetProjectMembersByRoleAsync(projectId, Roles.Submitter.ToString());

            List<TTUser> teamMembers = developers.Concat(submitters).Concat(admins).ToList();
            return teamMembers;
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
        // ADD #15 Services / Project Service (part 5)
        public async Task<TTUser> GetProjectManagerAsync(int projectId)
        {   // ADD #15 Services / Project Service (part 5)
            Project project = await _context.Projects
                                            .Include(p => p.Members)
                                            .FirstOrDefaultAsync(p => p.Id == projectId);
            foreach (TTUser member in project?.Members)
            {
                if (await _rolesService.IsUserInRoleAsync(member, Roles.ProjectManager.ToString()))
                {
                    return member;
                }
            }
            return null;
        }
        // ADD #14 Services / Project Service (part 4)
        public async Task<List<TTUser>> GetProjectMembersByRoleAsync(int projectId, string role)
        {   // ADD #14 Services / Project Service (part 4)
            Project project = await _context.Projects
                                            .Include(p => p.Members)
                                            .FirstOrDefaultAsync(p => p.Id == projectId);
            List<TTUser> members = new();
            foreach(var user in project.Members)
            {
                if(await _rolesService.IsUserInRoleAsync(user, role))
                {
                    members.Add(user);
                }
            }
            return members;
        }

        public Task<List<TTUser>> GetSubmittersOnProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }
        // ADD #13 Services / Project Service (part 3)
        public async Task<List<Project>> GetUserProjectsAsync(string userId)
        {   // ADD #13 Services / Project Service (part 3)
            try
            {
                List<Project> userProjects = (await _context.Users
                                            .Include(u => u.Projects)
                                                .ThenInclude(p => p.Company)
                                             .Include(u => u.Projects)
                                                .ThenInclude(p => p.Members)
                                            .Include(u => u.Projects)
                                                .ThenInclude(p => p.Tickets)
                                            .Include(u => u.Projects)
                                                 .ThenInclude(t => t.Tickets)
                                                     .ThenInclude(t => t.DeveloperUser)
                                            .Include(u => u.Projects)
                                                  .ThenInclude(t => t.Tickets)
                                                     .ThenInclude(t => t.OwnerUser)
                                            .Include(u => u.Projects)
                                                 .ThenInclude(t => t.Tickets)
                                                     .ThenInclude(t => t.TicketPriority)
                                            .Include(u => u.Projects)
                                                 .ThenInclude(t => t.Tickets)
                                                     .ThenInclude(t => t.TicketStatus)
                                            .Include(u => u.Projects)
                                                 .ThenInclude(t => t.Tickets)
                                                     .ThenInclude(t => t.TicketType)
                                            .FirstOrDefaultAsync(u => u.Id == userId)).Projects.ToList();
                return userProjects;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"*** ERROR, GREATSCOTT! *** - Error Getting user projects list. --> {ex.Message}");
                throw;
            }
        }
        // ADD #14 Services / Project Service (part 4)
        public async Task<List<TTUser>> GetUsersNotOnProjectAsync(int projectId, int companyId)
        {   // ADD #14 Services / Project Service (part 4)
            List<TTUser> users = await _context.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToListAsync();
            return users.Where(u => u.CompanyId == companyId).ToList();
        }
        // ADD #13 Services / Project Service (part 3)
        public async Task<bool> IsUserOnProjectAsync(string userId, int projectId)
        {   // ADD #13 Services / Project Service (part 3)
            Project project = await _context.Projects
                                            .Include(p => p.Members)
                                            .FirstOrDefaultAsync(p => p.Id == projectId);
            bool result = false;
            if (project != null) 
            {
                result = project.Members.Any(m => m.Id == userId);
            }
            return result;
        }
        // ADD #12 Services / Project Service (part 2)
        public async Task<int> LookupProjectPriorityId(string priorityName)
        {   // ADD #12 Services / Project Service (part 2)
            int priorityId = (await _context.ProjectPriorities.FirstOrDefaultAsync(p => p.Name == priorityName)).Id;
            return priorityId;
        }
        // ADD #15 Services / Project Service (part 5)
        public async Task RemoveProjectManagerAsync(int projectId)
        {   // ADD #15 Services / Project Service (part 5)
            Project project = await _context.Projects
                                            .Include(p => p.Members)
                                            .FirstOrDefaultAsync(p => p.Id == projectId);
            try
            {
                foreach(TTUser member in project?.Members)
                {
                    if (await _rolesService.IsUserInRoleAsync(member, Roles.ProjectManager.ToString()))
                    {
                        await RemoveUserFromProjectAsync(member.Id, projectId);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        // ADD #13 Services / Project Service (part 3)
        public async Task RemoveUserFromProjectAsync(string userId, int projectId)
        {   // ADD #13 Services / Project Service (part 3)
            try
            {
                TTUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                Project project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
                try
                {
                    if (await IsUserOnProjectAsync(userId, projectId))
                    {
                        project.Members.Remove(user);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"*** ERROR, GREATSCOTT! *** - Error Removing User from project. ---> {ex.Message}");
            }
        }
        // ADD #14 Services / Project Service (part 4)
        public async Task RemoveUsersFromProjectByRoleAsync(string role, int projectId)
        {   // ADD #14 Services / Project Service (part 4)
            try
            {
                List<TTUser> members = await GetProjectMembersByRoleAsync(projectId, role);
                Project project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
                foreach(TTUser ttUser in members)
                {
                    try
                    {
                        project.Members.Remove(ttUser);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"*** ERROR, GREATSCOTT! *** - Error Removing Users from project. ---> {ex.Message}");
                throw;
            }
        }
        // ADD #11 Services / Project Service (part 1) C.R.U.D. - UPDATE
        public async Task UpdateProjectAsync(Project project)
        {   // ADD #11 Services / Project Service (part 1)
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}