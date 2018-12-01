using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using WebCore.EntityFramework.Data;
using WebCore.Entities;

namespace WebCore.EntityFramework.Helper
{
    public class WebCoreRoleStore : RoleStore<WebCoreRole
                                                , WebCoreDbContext
                                                , string
                                                , WebCoreUserRole
                                                , WebCoreRoleClaim
                                                >
                                    , IRoleStore<WebCoreRole>
                                    , IDisposable
    {
        public WebCoreRoleStore(WebCoreDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
    }
}
