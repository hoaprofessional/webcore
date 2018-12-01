using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using WebCore.EntityFramework.Repositories;
using WebCore.Entities;
using WebCore.Services.Share.AppMenus;
using WebCore.Services.Share.AppMenus.Dto;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.AppMenus
{
    public class AppMenuService : IAppMenuService
    {
        private readonly IRepository<AppMenu, int> appMenuRepository;
        private readonly IMapper mapper;

        public AppMenuService(IRepository<AppMenu, int> appMenuRepository, IMapper mapper)
        {
            this.appMenuRepository = appMenuRepository;
            this.mapper = mapper;
        }

        public ListResult<AppMenuDto> GetAllMenuByPermission(List<string> permissions)
        {
            var menuQuery = appMenuRepository.GetByCondition(x => x.RecordStatus == ConstantConfig.RecordStatusConfig.Active && permissions.Contains(x.Permission));
            var menuResult = menuQuery
                .ProjectTo<AppMenuDto>(mapper.ConfigurationProvider).ToList();
            Dictionary<int, AppMenuDto> appMenuDic =
                menuResult
                .ToDictionary(x => x.Id);


            foreach(var menu in menuResult)
            {
                menu.Childs = new List<AppMenuDto>();
                if (menu.ParentId>0)
                {
                    appMenuDic[menu.ParentId].Childs.Add(menu);
                }
            }
            menuResult = menuResult.Where(x => x.ParentId == 0).ToList();

            return new ListResult<AppMenuDto>()
            {
                DataList = menuResult
            };
        }
    }
}
