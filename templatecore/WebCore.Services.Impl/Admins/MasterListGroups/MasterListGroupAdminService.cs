using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Admins.MasterListGroups;
using WebCore.Services.Share.Admins.MasterListGroups.Dto;
using WebCore.Services.Share.Languages;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.Admins.MasterListGroups
{
    public class MasterListGroupAdminService : BaseService, IMasterListGroupAdminService
    {
        private readonly IRepository<MasterList, int> masterListRepository;
        private readonly ILanguageProviderService languageProviderService;
        private readonly IMapper mapper;
        public MasterListGroupAdminService(IServiceProvider serviceProvider,
            IMapper mapper,
            ILanguageProviderService languageProviderService,
            IRepository<MasterList, int> masterListRepository) : base(serviceProvider)
        {
            this.masterListRepository = masterListRepository;
            this.mapper = mapper;
            this.languageProviderService = languageProviderService;
        }

        //public SelectList GetMasterListGroupsCombobox()
        //{
        //    return masterListRepository.GetAll().Select(x => new ComboboxResult<int, string>()
        //    {
        //        Value = x.Id,
        //        Display = $"{x.Name} - {languageProviderService.GetlangByKey($"LBL_ADMINMENUITEM_{x.Name}")}"
        //    }).ToList().ToSelectList();
        //}

        public PagingResultDto<MasterListGroupDto> GetAllByPaging(MasterListGroupFilterInput masterListFilterInput)
        {
            // neu khong truyen page size thi lay pagesize mac dinh trong bang appparameter
            SetDefaultPageSize(masterListFilterInput);

            IQueryable<MasterListGroupDto> query = masterListRepository.GetAll()
                                                    .Where(x => x.Group == ConstantConfig.MasterListMasterGroup)
                                                    .Filter(masterListFilterInput)
                                                    .OrderBy(x => x.OrderNo)
                                                    .ProjectTo<MasterListGroupDto>(mapper.ConfigurationProvider);

            return query.PagedQuery(masterListFilterInput);
        }

        public MasterList GetById(EntityId<int> idModel)
        {
            if (idModel == null)
            {
                return null;
            }
            return masterListRepository.GetById(idModel.Id);
        }

        public MasterListGroupInput GetInputById(EntityId<int> idModel)
        {
            if(idModel==null)
            {
                return null;
            }
            MasterList entity = masterListRepository.GetAll()
                               .Where(x => x.Group == ConstantConfig.MasterListMasterGroup && x.Id == idModel.Id).SingleOrDefault();
            if(entity==null)
            {
                return null;
            }
            return mapper.Map<MasterListGroupInput>(entity);
        }

        public MasterListGroupInput Add(MasterListGroupInput inputModel)
        {
            MasterList entity = mapper.Map<MasterList>(inputModel);
            entity.Group = ConstantConfig.MasterListMasterGroup;
            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;
            entity.UpdateToken = Guid.NewGuid();

            if (entity.OrderNo == null)
            {
                entity.OrderNo = masterListRepository
                                .GetByCondition(x => x.Group == ConstantConfig.MasterListMasterGroup)
                                .OrderByDescending(x => x.OrderNo)
                                .Select(x => x.OrderNo).FirstOrDefault().GetValueOrDefault(0)+1;
            }

            masterListRepository.Add(entity);
            return mapper.Map<MasterListGroupInput>(entity);
        }

        public bool Update(MasterListGroupInput inputModel)
        {
            MasterList entity = GetById(inputModel);
            if (entity == null)
            {
                return false;
            }
            mapper.Map(inputModel, entity);
            entity.Group = ConstantConfig.MasterListMasterGroup;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.UpdateToken = Guid.NewGuid();
            masterListRepository.Update(entity);
            return true;
        }

        public bool Delete(EntityId<int> idModel)
        {
            MasterList entity = GetById(idModel);
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
            masterListRepository.Update(entity);
            return true;
        }

        public bool Restore(EntityId<int> idModel)
        {
            MasterList entity = GetById(idModel);
            if (entity == null)
            {
                return false;
            }
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.UpdateToken = Guid.NewGuid();
            masterListRepository.Update(entity);
            return true;
        }
    }
}
