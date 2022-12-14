using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context; /*private properties*/
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

        #region GetRoles
        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            try
            {
                List<IdentityRole> result = new();
                result = await _context.Roles.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region AddUserToRole
        // MODIFY #4 Services / Role Services (part 3)
        public async Task<bool> AddUserToRoleAsync(TTUser user, string roleName) 
        {   // MODIFY #4 Services / Role Services (part 3)
            //throw new NotImplementedException();
            bool result = (await _userManager.AddToRoleAsync(user, roleName)).Succeeded;
            return result;
        }
        #endregion

        #region GetRoleNameById
        // MODIFY #5 Services / Role Services (part 4)
        public async Task<string> GetRoleNameByIdAsync(string roleId)
        {   // MODIFY #5 Services / Role Services (part 4)
            //throw new NotImplementedException();
            IdentityRole role = _context.Roles.Find(roleId);
            string result = await _roleManager.GetRoleNameAsync(role);
            return result;
        }
        #endregion

        #region  GetUserRoles
        // MODIFY #5 Services / Role Services (part 4)
        public async Task<IEnumerable<string>> GetUserRolesAsync(TTUser user)
        {    // MODIFY #5 Services / Role Services (part 4)
            //throw new NotImplementedException();
            IEnumerable<string> result = await _userManager.GetRolesAsync(user);
            return result;
        }
        #endregion

        #region GetUsersInRole
        // MODIFY #5 Services / Role Services (part 4)
        public async Task<List<TTUser>> GetUsersInRoleAsync(string roleName, int companyId)
        {   // MODIFY #5 Services / Role Services (part 4)
            //throw new NotImplementedException();
            List<TTUser> users = (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
            List<TTUser> result = users.Where(u => u.CompanyId == companyId).ToList();
            return result;
        }
        #endregion

        #region GetUsersNotInRole
        // MODIFY #5 Services / Role Services (part 4)
        public async Task<List<TTUser>> GetUsersNotInRoleAsync(string roleName, int companyId)
        {   // MODIFY #5 Services / Role Services (part 4)
            //throw new NotImplementedException();
            List<string> userIds = (await _userManager.GetUsersInRoleAsync(roleName)).Select(u => u.Id).ToList();
            List<TTUser> roleUsers = _context.Users.Where(u => !userIds.Contains(u.Id)).ToList();
            List<TTUser> result = roleUsers.Where(u => u.CompanyId == companyId).ToList();
            return result;
        }
        #endregion

        #region IsUserInRole
        // MODIFY #5 Services / Role Services (part 4)
        public async Task<bool> IsUserInRoleAsync(TTUser user, string roleName)
        {   // MODIFY #5 Services / Role Services (part 4)
            //throw new NotImplementedException();
            bool result = await _userManager.IsInRoleAsync(user, roleName);
            return result;
        }
        #endregion

        #region RemoveUserFromRole
        // MODIFY #5 Services / Role Services (part 4)
        public async Task<bool> RemoveUserFromRoleAsync(TTUser user, string roleName)
        {   // MODIFY #5 Services / Role Services (part 4)
            //throw new NotImplementedException();
            bool result = (await _userManager.RemoveFromRoleAsync(user, roleName)).Succeeded;
            return result;
        }
        #endregion

        #region RemoveUserFromRoles
        // MODIFY #5 Services / Role Services (part 4)
        public async Task<bool> RemoveUserFromRolesAsync(TTUser user, IEnumerable<string> roles)
        {   // MODIFY #5 Services / Role Services (part 4)
            //throw new NotImplementedException();
            bool result = (await _userManager.RemoveFromRolesAsync(user, roles)).Succeeded;
            return result;
        }
        #endregion
    }
}