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

namespace TheTracker.Controllers
{
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

        public async Task <IActionResult> ManageUserRoles() // Action Name
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
    }
}