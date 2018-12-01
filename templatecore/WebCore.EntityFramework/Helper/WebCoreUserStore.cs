using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebCore.EntityFramework.Data;
using WebCore.Entities;

namespace WebCore.EntityFramework.Helper
{
    public class WebCoreUserStore : UserStore<WebCoreUser
                                                , WebCoreRole
                                                , WebCoreDbContext
                                                , string
                                                , WebCoreUserClaim
                                                , WebCoreUserRole
                                                , WebCoreUserLogin
                                                , WebCoreUserToken
                                                , WebCoreRoleClaim
                                             >
                                    , IUserStore<WebCoreUser>
                                    , IDisposable
    {
        public WebCoreUserStore(WebCoreDbContext context) : base(context) { }
    }
}
