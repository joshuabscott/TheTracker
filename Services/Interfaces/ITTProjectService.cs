using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Models;
// ADD #10 Services / Project Service (Intro)
namespace TheTracker.Services.Interfaces
{
    public interface ITTProjectService
    {   // ADD #11 Services / Project Service (part 1)
        public Task AddNewProjectAsync(Project project);
        public Task<bool> AddProjectManagerAsync(string userId, int projectId);
        public Task<bool> AddUserToProjectAsync(string userId, int projectId);

        public Task ArchiveProjectAsync(Project project);

        public Task<List<Project>> GetAllProjectsByCompany(int companyId);
        public Task<List<Project>> GetAllProjectsByPriority(int companyId, string priorityName);
        public Task<List<TTUser>> GetAllProjectMembersExceptPMAsync(int projectId);

        public Task<List<Project>> GetArchivedProjectsByCompany(int companyId);

        public Task<List<TTUser>> GetDevelopersOnProjectAsync(int projectId);
        public Task<TTUser> GetProjectManagerAsync(int projectId);
        public Task<List<TTUser>> GetProjectMembersByRoleAsync(int projectId, string role);

        public Task<Project> GetProjectByIdAsync(int projectId, int companyId);

        public Task<List<TTUser>> GetSubmittersOnProjectAsync(int projectId);
        public Task<List<TTUser>> GetUsersNotOnProjectAsync(int projectId, int companyId);
        public Task<List<Project>> GetUserProjectsAsync(string userId);

        public Task<bool> IsUserOnProjectAsync(string userId, int projectId);

        public Task<int> LookupProjectPriorityId(string priorityName);

        public Task RemoveProjectManagerAsync(int projectId);
        public Task RemoveUsersFromProjectByRoleAsync(string role, int projectId);
        public Task RemoveUserFromProjectAsync(string userId, int projectId);

        public Task UpdateProjectAsync(Project project);
    }
}