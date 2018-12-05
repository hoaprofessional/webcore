using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Admins.AdminMenus;
using WebCore.Services.Share.Admins.AdminMenus.Dto;
using WebCore.Services.Share.Languages;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Commons;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.Admins.AdminMenus
{
    public class AdminMenuAdminService : BaseService, IAdminMenuAdminService
    {
        private readonly IRepository<AdminMenu, int> adminMenuRepository;
        readonly ILanguageProviderService languageProviderService;
        private readonly IMapper mapper;
        public AdminMenuAdminService(IServiceProvider serviceProvider,
            IMapper mapper,
            ILanguageProviderService languageProviderService,
            IRepository<AdminMenu, int> adminMenuRepository) : base(serviceProvider)
        {
            this.adminMenuRepository = adminMenuRepository;
            this.mapper = mapper;
            this.languageProviderService = languageProviderService;
        }

        public SelectList GetAdminMenusCombobox()
        {
            return adminMenuRepository.GetAll().Select(x => new ComboboxResult<int, string>()
            {
                Value = x.Id,
                Display = $"{x.Name} - {languageProviderService.GetlangByKey($"LBL_ADMINMENUITEM_{x.Name}")}"
            }).ToList().ToSelectList();
        }

        public PagingResultDto<AdminMenuDto> GetAllByPaging(AdminMenuFilterInput adminMenuFilterInput)
        {
            // neu khong truyen page size thi lay pagesize mac dinh trong bang appparameter
            SetDefaultPageSize(adminMenuFilterInput);

            IQueryable<AdminMenuDto> query = adminMenuRepository.GetAll()
                                                    .Filter(adminMenuFilterInput)
                                                    .OrderBy(x => x.OrderNo)
                                                    .ProjectTo<AdminMenuDto>(mapper.ConfigurationProvider);

            return query.PagedQuery(adminMenuFilterInput);
        }

        public AdminMenu GetById(EntityId<int> idModel)
        {
            if (idModel == null)
            {
                return null;
            }
            return adminMenuRepository.GetById(idModel.Id);
        }

        public AdminMenuInput GetInputById(EntityId<int> idModel)
        {
            AdminMenu entity = GetById(idModel);
            return mapper.Map<AdminMenuInput>(entity);
        }

        public AdminMenuInput AddAdminMenu(AdminMenuInput inputModel)
        {
            AdminMenu entity = mapper.Map<AdminMenu>(inputModel);
            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;
            entity.UpdateToken = Guid.NewGuid();
            adminMenuRepository.Add(entity);
            return mapper.Map<AdminMenuInput>(entity);
        }

        public bool UpdateAdminmenu(AdminMenuInput inputModel)
        {
            AdminMenu entity = GetById(inputModel);
            if (entity == null)
            {
                return false;
            }
            mapper.Map(inputModel, entity);
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.UpdateToken = Guid.NewGuid();
            adminMenuRepository.Update(entity);
            return true;
        }

        public bool DeleteAdminMenu(EntityId<int> idModel)
        {
            AdminMenu entity = GetById(idModel);
            if (entity == null)
            {
                return false;
            }
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Deleted;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.DeletedBy = entity.ModifiedBy;
            entity.DeletedDate = entity.ModifiedDate;
            entity.UpdateToken = Guid.NewGuid();
            adminMenuRepository.Update(entity);
            return true;
        }

        public bool RestoreAdminMenu(EntityId<int> idModel)
        {
            AdminMenu entity = GetById(idModel);
            if (entity == null)
            {
                return false;
            }
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.UpdateToken = Guid.NewGuid();
            adminMenuRepository.Update(entity);
            return true;
        }
    }
}
