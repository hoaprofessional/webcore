using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Admins.LanguageDetails;
using WebCore.Services.Share.Admins.LanguageDetails.Dto;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.Helpers;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.Admins.LanguageDetails
{
    public class LanguageDetailAdminService : BaseService, ILanguageDetailAdminService
    {
        private IMapper mapper;
        private readonly IRepository<LanguageDetail, int> languageDetailRepository;
        public LanguageDetailAdminService(IServiceProvider serviceProvider, IMapper mapper, IRepository<LanguageDetail, int> languageRepository) : base(serviceProvider)
        {
            languageDetailRepository = languageRepository;
            this.mapper = mapper;
        }

        public PagingResultDto<LanguageDetailDto> GetAllByPaging(LanguageDetailFilterInput languageFilterInput)
        {
            SetDefaultPageSize(languageFilterInput);
            IQueryable<LanguageDetail> query = languageDetailRepository.GetByCondition(x => x.LanguageCode == languageFilterInput.LangCode);

            query = query.Filter(languageFilterInput);

            IQueryable<LanguageDetailDto> queryDto = query.ProjectTo<LanguageDetailDto>(mapper.ConfigurationProvider);
            return queryDto.PagedQuery(languageFilterInput);
        }

        public LanguageDetailInput Add(LanguageDetailInput input)
        {
            LanguageDetail entity = mapper.Map<LanguageDetail>(input);
            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            languageDetailRepository.Add(entity);
            return mapper.Map<LanguageDetailInput>(entity);
        }

        public bool Update(LanguageDetailInput input)
        {
            LanguageDetail entity = GetById(input);
            if (entity == null)
            {
                return false;
            }
            mapper.Map(input, entity);
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetCurrentUserLogin();
            languageDetailRepository.Update(entity);
            return true;
        }

        public bool Delete(EntityId<int> idModel)
        {
            LanguageDetail entity = GetById(idModel);
            if (entity == null)
            {
                return false;
            }
            entity.DeletedDate = DateTime.Now;
            entity.DeletedBy = GetCurrentUserLogin();
            languageDetailRepository.Update(entity);
            return true;
        }

        public LanguageDetailInput GetInputById(EntityId<int> idModel)
        {
            LanguageDetail entity = GetById(idModel);
            return mapper.Map<LanguageDetailInput>(entity);
        }

        public LanguageDetail GetById(EntityId<int> idModel)
        {
            return languageDetailRepository.GetById(idModel.Id);
        }


    }
}
