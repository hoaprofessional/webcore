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
using System.Linq.Dynamic.Core;

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
        

        public SortingAndPagingResultDto<MasterListDto> GetAllByPaging(MasterListFilterInput masterListFilterInput)
        {
            // neu khong truyen page size thi lay pagesize mac dinh trong bang appparameter
            SetDefaultPageSize(masterListFilterInput);

            if(masterListFilterInput.Sorting==null)
            {
                masterListFilterInput.Sorting = "ModifiedDate desc";
            }

            IQueryable<MasterListDto> query = masterListRepository.GetAll()
                                                    .Filter(masterListFilterInput)
                                                    .OrderBy(masterListFilterInput.Sorting)
                                                    .ProjectTo<MasterListDto>(mapper.ConfigurationProvider);

            return query.PagedAndSortingQuery(masterListFilterInput);
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

        public MasterListInput Add(MasterListInput inputModel)
        {
            var entity = mapper.Map<MasterList>(inputModel);
            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;
            entity.UpdateToken = Guid.NewGuid();

            if (entity.OrderNo == null)
            {
                entity.OrderNo = masterListRepository
                                .GetByCondition(x => x.Group == entity.Group)
                                .OrderByDescending(x => x.OrderNo)
                                .Select(x => x.OrderNo).FirstOrDefault().GetValueOrDefault(0) + 1;
            }

            entity = masterListRepository.Add(entity);
            return mapper.Map<MasterListInput>(entity);
        }

        public bool Update(MasterListInput inputModel)
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

        public bool Delete(EntityId<int> idModel)
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

        public bool Restore(EntityId<int> idModel)
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
    }
}
