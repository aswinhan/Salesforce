using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalesforceWeb.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace SalesforceWeb.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _screen;
        private readonly string _permission;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HasPermissionAttribute(string screen, string permission, IHttpContextAccessor httpContextAccessor)
        {
            _screen = screen;
            _permission = permission;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString(StaticData.SessionToken);

            if (string.IsNullOrEmpty(sessionToken))
            {
                context.Result = new ForbidResult();
                return;
            }

            // Decode the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(sessionToken) as JwtSecurityToken;

            if (jwtToken == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Extract the claims from the JWT token
            var claims = jwtToken.Claims;

            // Retrieve the permission claim for the specific screen
            var permissionClaim = claims.FirstOrDefault(c => c.Type == $"Permission_{_screen}");

            if (permissionClaim == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            var hasPermission = permissionClaim.Value.Split(',').Contains(_permission);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
