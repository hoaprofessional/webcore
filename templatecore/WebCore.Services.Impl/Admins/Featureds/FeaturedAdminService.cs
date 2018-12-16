using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Admins.Featureds;
using WebCore.Services.Share.Admins.Featureds.Dto;
using WebCore.Services.Share.Languages;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Commons;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;
using System.Linq.Dynamic.Core;

namespace WebCore.Services.Impl.Admins.Featureds
{
    public class FeaturedAdminService : BaseService, IFeaturedAdminService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Featured, int> masterListRepository;
        public FeaturedAdminService(IServiceProvider serviceProvider,
            IMapper mapper,
            IRepository<Featured, int> masterListRepository) : base(serviceProvider)
        {
            this.masterListRepository = masterListRepository;
            this.mapper = mapper;
        }
        

        public SortingAndPagingResultDto<FeaturedDto> GetAllByPaging(FeaturedFilterInput filterInput)
        {
            // neu khong truyen page size thi lay pagesize mac dinh trong bang appparameter
            SetDefaultPageSize(filterInput);

            if(filterInput.Sorting==null)
            {
                filterInput.Sorting = "ModifiedDate desc";
            }

            IQueryable<FeaturedDto> query = masterListRepository.GetAll()
                                                    .Filter(filterInput)
                                                    .OrderBy(filterInput.Sorting)
                                                    .ProjectTo<FeaturedDto>(mapper.ConfigurationProvider);

            return query.PagedAndSortingQuery(filterInput);
        }

        public Featured GetById(EntityId<int> idModel)
        {
            if(idModel==null)
            {
                return null;
            }
            return masterListRepository.GetById(idModel.Id);
        }

        public FeaturedInput GetInputById(EntityId<int> idModel)
        {
            Featured entity = GetById(idModel);
            return mapper.Map<FeaturedInput>(entity);
        }

        public FeaturedInput Add(FeaturedInput inputModel)
        {
            var entity = mapper.Map<Featured>(inputModel);
            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;
            entity.UpdateToken = Guid.NewGuid();
            masterListRepository.Add(entity);
            return mapper.Map<FeaturedInput>(entity);
        }

        public bool Update(FeaturedInput inputModel)
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
