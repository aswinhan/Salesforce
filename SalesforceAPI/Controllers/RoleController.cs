using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Data;
using SalesforceAPI.Dtos;
using SalesforceAPI.Models;
using System.Net;

namespace SalesforceAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected APIResponse _response;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
            _response = new APIResponse();
        }

        [HttpGet(Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetAllRoles()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();

                if (roles == null || !roles.Any())
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No roles found.");
                    return NotFound(_response);
                }

                var roleDtos = roles.Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();

                _response.Result = roleDtos;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("An error occurred while fetching roles.");
                _response.ErrorMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }

        [HttpGet("{roleId}", Name = "GetRolePermissions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRolePermissions(string roleId)
        {
            try
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Role not found.");
                    return NotFound(_response);
                }

                var rolePermissions = await _context.RolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .Include(rp => rp.Screen)
                    .Include(rp => rp.Permission)
                    .ToListAsync();

                var allScreens = await _context.Screens.ToListAsync();
                var allPermissions = await _context.Permissions.ToListAsync();

                var result = allScreens.Select(screen => new RolePermissionResponseDto
                {
                    ScreenId = screen.Id,
                    ScreenName = screen.Name,
                    Permissions = allPermissions.Select(permission => new PermissionCheckboxDto
                    {
                        Id = permission.Id,
                        Name = permission.Name,
                        IsSelected = rolePermissions.Any(rp => rp.ScreenId == screen.Id && rp.PermissionId == permission.Id)
                    }).ToList()
                }).ToList();

                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("An error occurred while fetching role permissions.");
                _response.ErrorMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> AddOrUpdateRolePermissions([FromBody] List<RolePermissionDto> rolePermissionDtos)
        {
            try
            {
                if (rolePermissionDtos == null || !ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Invalid role permissions data.");
                    return BadRequest(_response);
                }

                var roleId = rolePermissionDtos.FirstOrDefault()?.RoleId;

                if (string.IsNullOrEmpty(roleId))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Role ID is required.");
                    return BadRequest(_response);
                }

                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Role not found.");
                    return NotFound(_response);
                }

                var existingPermissions = await _context.RolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .ToListAsync();

                var newPermissions = rolePermissionDtos
                    .SelectMany(rp => rp.PermissionIds.Select(pid => new RolePermission
                    {
                        RoleId = rp.RoleId,
                        ScreenId = rp.ScreenId,
                        PermissionId = pid
                    })).ToList();

                var permissionsToRemove = existingPermissions
                    .Where(ep => !newPermissions.Any(np => np.ScreenId == ep.ScreenId && np.PermissionId == ep.PermissionId))
                    .ToList();

                var permissionsToAdd = newPermissions
                    .Where(np => !existingPermissions.Any(ep => ep.ScreenId == np.ScreenId && ep.PermissionId == np.PermissionId))
                    .ToList();

                _context.RolePermissions.RemoveRange(permissionsToRemove);
                await _context.RolePermissions.AddRangeAsync(permissionsToAdd);

                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                await _context.SaveChangesWithAuditAsync(user.Id);

                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = rolePermissionDtos;
                return CreatedAtRoute("GetRolePermissions", new { roleId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("An error occurred while updating role permissions.");
                _response.ErrorMessages.Add(ex.ToString());
                return BadRequest(_response);
            }
        }

    }
}
