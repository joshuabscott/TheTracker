using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Models;
// ADD #2 Services / Role Services (part 1)
namespace TheTracker.Services.Interfaces
{
    public interface ITTRolesService
    {
        public Task<bool> IsUserInRoleAsync(TTUser user, string roleName);
        public Task<IEnumerable<string>> GetUserRolesAsync(TTUser user);
        public Task<bool> AddUserToRoleAsync(TTUser user, string roleName);
        public Task<bool> RemoveUserFromRoleAsync(TTUser user, string roleName);
        public Task<bool> RemoveUserFromRoleAsync(TTUser user, IEnumerable<string> roles);
        public Task<List<TTUser>> GetUsersInRoleAsync(string roleName, int companyId);
        public Task<List<TTUser>> GetUsersNotInRoleAsync(string roleName, int companyId);
        public Task<string> GetRoleNameByIdAsync(string roleId);
    }
}
