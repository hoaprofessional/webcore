using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using WebCore.EntityFramework.Repositories;
using WebCore.Entities;
using WebCore.Services.Share.Admins.Languages;
using WebCore.Services.Share.Admins.Languages.Dto;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Helpers;
using WebCore.Utils.ModelHelper;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using WebCore.Utils.Config;

namespace WebCore.Services.Impl.Admins.Languages
{
    public class LanguageAdminService : BaseService, ILanguageAdminService
    {
        IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private readonly IRepository<Language, int> languageRepository;
        public LanguageAdminService(IServiceProvider serviceProvider, 
            IMemoryCache memoryCache, 
            IMapper mapper, 
            IRepository<Language, int> languageRepository) : base(serviceProvider)
        {
            this.languageRepository = languageRepository;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        public List<LanguageDto> GetAllLanguages()
        {
            List<LanguageDto> languageInCache = memoryCache.Get<List<LanguageDto>>(ConstantConfig.MemoryCacheConfig.LanguageSelectCache);
            if (languageInCache==null)
            {
                IQueryable<Language> query = languageRepository.GetAll();
                IQueryable<LanguageDto> queryDto = query.ProjectTo<LanguageDto>(mapper.ConfigurationProvider);
                languageInCache = queryDto.ToList();
                memoryCache.Set(ConstantConfig.MemoryCacheConfig.LanguageSelectCache, languageInCache);
            }
            return languageInCache;
        }

        public PagingResultDto<LanguageDto> GetAllByPaging(LanguageFilterInput languageFilterInput)
        {
            // neu khong truyen page size thi lay pagesize mac dinh trong bang appparameter
            SetDefaultPageSize(languageFilterInput);
            IQueryable<Language> query = languageRepository.GetAll();

            #region FILTER BEGIN
            
            // filter by lang code

            if(!languageFilterInput.LangCode.IsNullOrWhiteSpace())
            {
                query = query.Where(x => x.LangCode.ToLower().Contains(languageFilterInput.LangCode.ToLower()));
            }

            // filter by lang name

            if (!languageFilterInput.LangName.IsNullOrWhiteSpace())
            {
                query = query.Where(x => x.LangName.ToLower().Contains(languageFilterInput.LangName.ToLower()));
            }

            #endregion

            IQueryable<LanguageDto> queryDto = query.ProjectTo<LanguageDto>(mapper.ConfigurationProvider);
            return queryDto.PagedQuery(languageFilterInput);
        }
    }
}
