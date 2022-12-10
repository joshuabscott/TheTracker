using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheTracker.Extensions;
using TheTracker.Models;
using TheTracker.Models.ViewModels;
using TheTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace TheTracker.Controllers
{
    [Authorize]
    public class UserRolesController : Controller // Implementation
    {
        private readonly ITTRolesService _rolesService;
        private readonly ITTCompanyInfoService _companyInfoService;

        public UserRolesController(ITTRolesService rolesService, 
                                   ITTCompanyInfoService companyInfoService)
        {
            _rolesService = rolesService;
            _companyInfoService = companyInfoService;
        }

        #region GET ManageUserRole
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles() // Action Name
        {
            // Add an instance of the ViewModel as a List (model)
            List<ManageUserRolesViewModel> model = new();

            // Get CompanyId
            int companyId = User.Identity.GetCompanyId().Value;

            // Get all company users
            List<TTUser> users = await _companyInfoService.GetAllMembersAsync(companyId);

            // Loop over the users to populate the ViewModel
            foreach (TTUser user in users)
            {
                // - instantiate ViewModel
                ManageUserRolesViewModel viewModel = new();
                viewModel.TTUser = user;
                // - use _rolesService
                IEnumerable<string> selected = await _rolesService.GetUserRolesAsync(user);
                // - Create multi-selects
                viewModel.Roles = new MultiSelectList(await _rolesService.GetRolesAsync(), "Name", "Name", selected);

                model.Add(viewModel);
            }

            // Return the model to the View
            return View(model);
        }
        #endregion


        #region POST ManageUserRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(ManageUserRolesViewModel member) // Action Name
        {
            // Get Company Id
            int companyId = User.Identity.GetCompanyId().Value;

            // Instantiate the TTUser
            TTUser ttUser = (await _companyInfoService.GetAllMembersAsync(companyId)).FirstOrDefault(u => u.Id == member.TTUser.Id);

            // Get Roles for the User
            IEnumerable<string> roles = await _rolesService.GetUserRolesAsync(ttUser);

            // Grab the selected Role
            string userRole = member.SelectedRoles.FirstOrDefault();

            if (!string.IsNullOrEmpty(userRole))
            {
                // Remove User from their Roles
                if (await _rolesService.RemoveUserFromRolesAsync(ttUser, roles))
                {
                    // Add User to the New Role
                    await _rolesService.AddUserToRoleAsync(ttUser, userRole);
                }
            }

            // Navigate back to the View
            return RedirectToAction(nameof(ManageUserRoles));
        } 
        #endregion

    }
}