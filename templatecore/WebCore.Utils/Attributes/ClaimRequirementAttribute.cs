using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebCore.Utils.Config;

namespace WebCore.Utils.Attributes
{ 

public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    {
        claimValue = "," + claimValue + ",";
        Arguments = new object[] { new Claim(claimType.ToString(), claimValue) };
    }
    public ClaimRequirementAttribute(string claimValue)
        : this(ConstantConfig.ClaimType.Permission, claimValue)
    {

    }
}
}
