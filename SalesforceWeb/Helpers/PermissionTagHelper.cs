using Microsoft.AspNetCore.Razor.TagHelpers;
using SalesforceWeb.Utilities;
using System.IdentityModel.Tokens.Jwt;

namespace SalesforceWeb.Helpers
{
    [HtmlTargetElement("*", Attributes = "asp-screen, asp-permission")]
    public class PermissionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("asp-screen")]
        public string Screen { get; set; }

        [HtmlAttributeName("asp-permission")]
        public string Permission { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString(StaticData.SessionToken);

            if (string.IsNullOrEmpty(sessionToken))
            {
                output.SuppressOutput();
                return;
            }

            // Decode the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(sessionToken) as JwtSecurityToken;

            if (jwtToken == null)
            {
                output.SuppressOutput();
                return;
            }

            // Extract the claims from the JWT token
            var claims = jwtToken.Claims;

            // Retrieve the permission claim for the specific screen
            var permissionClaim = claims.FirstOrDefault(c => c.Type == $"Permission_{Screen}");

            if (permissionClaim == null)
            {
                output.SuppressOutput();
                return;
            }

            var hasPermission = permissionClaim.Value.Split(',').Contains(Permission);

            if (!hasPermission)
            {
                output.SuppressOutput();
            }
        }
    }
}
