using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Data;
using TheTracker.Models;
using TheTracker.Services.Interfaces;
// ADD #3 Services / Role Services (part 2)
namespace TheTracker.Services
{
    public class TTRolesService : ITTRolesService // Implement creates a baseline of scaffolded code to start with
    {
        private readonly ApplicationDbContext _context; /*private property*/
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<TTUser> _userManager;

        public TTRolesService(ApplicationDbContext context,
                               RoleManager<IdentityRole> roleManager,
                               UserManager<TTUser> userManager) /*method constructor*/
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Task<bool> AddUserToRoleAsync(TTUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserRolesAsync(TTUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<TTUser>> GetUsersInRoleAsync(string roleName, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TTUser>> GetUsersNotInRoleAsync(string roleName, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserInRoleAsync(TTUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRoleAsync(TTUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRoleAsync(TTUser user, IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
