using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Admins.MetaDescriptions;
using WebCore.Services.Share.Admins.MetaDescriptions.Dto;
using WebCore.Services.Share.Languages;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Commons;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;
using System.Linq.Dynamic.Core;

namespace WebCore.Services.Impl.Admins.MetaDescriptions
{
    public class MetaDescriptionAdminService : BaseService, IMetaDescriptionAdminService
    {
        private readonly IMapper mapper;
        private readonly IRepository<MetaDescription, int> masterListRepository;
        public MetaDescriptionAdminService(IServiceProvider serviceProvider,
            IMapper mapper,
            IRepository<MetaDescription, int> masterListRepository) : base(serviceProvider)
        {
            this.masterListRepository = masterListRepository;
            this.mapper = mapper;
        }
        

        public SortingAndPagingResultDto<MetaDescriptionDto> GetAllByPaging(MetaDescriptionFilterInput masterListFilterInput)
        {
            // neu khong truyen page size thi lay pagesize mac dinh trong bang appparameter
            SetDefaultPageSize(masterListFilterInput);

            if(masterListFilterInput.Sorting==null)
            {
                masterListFilterInput.Sorting = "ModifiedDate desc";
            }

            IQueryable<MetaDescriptionDto> query = masterListRepository.GetAll()
                                                    .Filter(masterListFilterInput)
                                                    .OrderBy(masterListFilterInput.Sorting)
                                                    .ProjectTo<MetaDescriptionDto>(mapper.ConfigurationProvider);

            return query.PagedAndSortingQuery(masterListFilterInput);
        }

        public MetaDescription GetById(EntityId<int> idModel)
        {
            if(idModel==null)
            {
                return null;
            }
            return masterListRepository.GetById(idModel.Id);
        }

        public MetaDescriptionInput GetInputById(EntityId<int> idModel)
        {
            MetaDescription entity = GetById(idModel);
            return mapper.Map<MetaDescriptionInput>(entity);
        }

        public MetaDescriptionInput Add(MetaDescriptionInput inputModel)
        {
            var entity = mapper.Map<MetaDescription>(inputModel);
            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;
            entity.UpdateToken = Guid.NewGuid();
            masterListRepository.Add(entity);
            return mapper.Map<MetaDescriptionInput>(entity);
        }

        public bool Update(MetaDescriptionInput inputModel)
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
