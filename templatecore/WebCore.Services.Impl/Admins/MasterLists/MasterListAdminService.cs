using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Admins.MasterLists;
using WebCore.Services.Share.Admins.MasterLists.Dto;
using WebCore.Services.Share.Languages;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Commons;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.Admins.MasterLists
{
    public class MasterListAdminService : BaseService, IMasterListAdminService
    {
        private readonly IMapper mapper;
        private readonly IRepository<MasterList, int> masterListRepository;
        public MasterListAdminService(IServiceProvider serviceProvider,
            IMapper mapper,
            IRepository<MasterList, int> masterListRepository) : base(serviceProvider)
        {
            this.masterListRepository = masterListRepository;
            this.mapper = mapper;
        }
        

        public PagingResultDto<MasterListDto> GetAllByPaging(MasterListFilterInput masterListFilterInput)
        {
            // neu khong truyen page size thi lay pagesize mac dinh trong bang appparameter
            SetDefaultPageSize(masterListFilterInput);

            IQueryable<MasterListDto> query = masterListRepository.GetAll()
                                                    .Filter(masterListFilterInput)
                                                    .OrderBy(x=>x.OrderNo)
                                                    .ProjectTo<MasterListDto>(mapper.ConfigurationProvider);

            return query.PagedQuery(masterListFilterInput);
        }

        public MasterList GetById(EntityId<int> idModel)
        {
            if(idModel==null)
            {
                return null;
            }
            return masterListRepository.GetById(idModel.Id);
        }

        public MasterListInput GetInputById(EntityId<int> idModel)
        {
            MasterList entity = GetById(idModel);
            return mapper.Map<MasterListInput>(entity);
        }

        public MasterListInput AddMasterList(MasterListInput inputModel)
        {
            var entity = mapper.Map<MasterList>(inputModel);
            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;
            entity.UpdateToken = Guid.NewGuid();
            masterListRepository.Add(entity);
            return mapper.Map<MasterListInput>(entity);
        }

        public bool UpdateAdminmenu(MasterListInput inputModel)
        {
            var entity = GetById(inputModel);
            if (entity == null)
            {
                return false;
            }
            mapper.Map(inputModel, entity);
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.UpdateToken = Guid.NewGuid();
            masterListRepository.Update(entity);
            return true;
        }

        public bool DeleteMasterList(EntityId<int> idModel)
        {
            var entity = GetById(idModel);
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

        public bool RestoreMasterList(EntityId<int> idModel)
        {
            var entity = GetById(idModel);
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

        public SelectList GetMasterListGroupCombobox()
        {
            throw new NotImplementedException();
        }
    }
}
