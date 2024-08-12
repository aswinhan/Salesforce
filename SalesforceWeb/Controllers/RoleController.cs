using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using SalesforceWeb.Dtos;
using SalesforceWeb.Models;
using SalesforceWeb.Services.IServices;
using SalesforceWeb.Utilities;

namespace SalesforceWeb.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;
        private readonly IToastNotification _toastNotification;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger, IToastNotification toastNotification)
        {
            _roleService = roleService;
            _logger = logger;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index(string roleId = null)
        {
            var viewModel = new RoleManagement();

            try
            {
                // Get all roles
                var response = await _roleService.GetAllRolesAsync<APIResponse>(HttpContext.Session.GetString(StaticData.SessionToken));

                if (response != null && response.IsSuccess)
                {
                    viewModel.Roles = JsonConvert.DeserializeObject<List<RoleDto>>(Convert.ToString(response.Result));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Error fetching roles.");
                }

                // If a role is selected, load its permissions
                if (!string.IsNullOrEmpty(roleId))
                {
                    var permissionsResponse = await _roleService.GetRolePermissionsAsync<APIResponse>(roleId, HttpContext.Session.GetString(StaticData.SessionToken));
                    if (permissionsResponse != null && permissionsResponse.IsSuccess)
                    {
                        viewModel.SelectedRolePermissions = JsonConvert.DeserializeObject<List<RolePermissionResponseDto>>(Convert.ToString(permissionsResponse.Result));
                        viewModel.SelectedRoleId = roleId;
                        viewModel.SelectedRoleName = viewModel.Roles.FirstOrDefault(r => r.Id == roleId)?.Name;
                    }
                    else
                    {
                        _toastNotification.AddErrorToastMessage("Error fetching role permissions.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching roles or permissions: {ErrorMessage}", ex.Message);
                _toastNotification.AddErrorToastMessage("An error occurred while fetching roles or permissions.");
                return StatusCode(500, "Internal server error occurred: " + ex.Message);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string roleId)
        {
            try
            {
                var response = await _roleService.GetRolePermissionsAsync<APIResponse>(roleId, HttpContext.Session.GetString(StaticData.SessionToken));

                if (response != null && response.IsSuccess)
                {
                    var model = JsonConvert.DeserializeObject<List<RolePermissionResponseDto>>(Convert.ToString(response.Result));
                    ViewBag.RoleId = roleId;
                    return View(model);
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Error fetching Role Permissions");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Error fetching Role Permissions");
                _logger.LogError(ex, "Error fetching Role Permissions : {ErrorMessage}", ex.Message);
                return StatusCode(500, "Internal server error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleManagement roleManagement)
        {
            try
            {
                // Create a list of RolePermissionDto from the roleManagement model
                var rolePermissionDtos = roleManagement.SelectedRolePermissions
                    .Select(rp => new RolePermissionDto
                    {
                        RoleId = roleManagement.SelectedRoleId,
                        ScreenId = rp.ScreenId,
                        PermissionIds = rp.Permissions.Where(p => p.IsSelected).Select(p => p.Id).ToList()
                    })
                    .ToList();

                var response = await _roleService.UpdateRolePermissionsAsync<APIResponse>(rolePermissionDtos, HttpContext.Session.GetString(StaticData.SessionToken));

                if (response != null && response.IsSuccess)
                {
                    _toastNotification.AddSuccessToastMessage("Role Permissions updated successfully");
                    return RedirectToAction(nameof(Index), new { roleId = roleManagement.SelectedRoleId });
                }

                _toastNotification.AddErrorToastMessage("Error updating Role Permissions");
                return RedirectToAction(nameof(Index), new { roleId = roleManagement.SelectedRoleId });
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Error updating Role Permissions");
                _logger.LogError(ex, "Error updating Role Permissions : {ErrorMessage}", ex.Message);
                return StatusCode(500, "Internal server error occurred: " + ex.Message);
            }
        }
    }
}