using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
namespace WebCore.Utils.Attributes
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && _claim.Value.Contains("," + c.Value + ","));
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
