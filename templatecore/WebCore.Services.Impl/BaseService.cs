using Microsoft.AspNetCore.Http;
using System;
using WebCore.Services.Share.SystemConfigs;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl
{
    public class BaseService
    {
        protected readonly IServiceProvider serviceProvider;
        protected ISystemConfigService systemConfigService;
        public BaseService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            systemConfigService = (ISystemConfigService)serviceProvider.GetService(typeof(ISystemConfigService));
        }
        public string GetCurrentUserLogin()
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));
            return httpContextAccessor.HttpContext.User?.Identity?.Name;
        }
        protected void SetDefaultPageSize(IPagingFilterDto pagingFilterDto)
        {
            if (pagingFilterDto.PageSize <= 0)
            {
                pagingFilterDto.PageSize = (int)systemConfigService.GetValueNumber(ConstantConfig.SystemConfigName.PageDefaultNumber);
            }
        }
    }
}
