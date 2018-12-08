using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.AdminMenus;
using WebCore.Services.Share.AdminMenus.Dto;
using WebCore.Utils.Config;
using WebCore.Utils.TreeViewHelper;

namespace WebCore.Services.Impl.AdminMenus
{
    public class AdminMenuProvider : IAdminMenuProvider
    {
        private IRepository<AdminMenu, int> adminMenuRepository;
        private readonly IMapper mapper;

        public AdminMenuProvider(IRepository<AdminMenu, int> adminMenuRepository, IMapper mapper)
        {
            this.adminMenuRepository = adminMenuRepository;
            this.mapper = mapper;
        }

        public AdminMenuTreeViewDto GetAdminMenuTreeView(string[] permissions, string currentLink)
        {
            List<AdminMenuTreeViewDto> adminMenus = adminMenuRepository
                .GetByCondition(x => x.RecordStatus == ConstantConfig.RecordStatusConfig.Active && string.IsNullOrEmpty(x.Permission) || (permissions.Contains(x.Permission)))
                .OrderBy(x => x.OrderNo)
                .ProjectTo<AdminMenuTreeViewDto>(mapper.ConfigurationProvider)
                .ToList();
            adminMenus.ForEach(x =>
            {
                x.IsActive = (x.Link == currentLink);
            });
            return adminMenus.ToTreeView(0);
        }
    }
}
