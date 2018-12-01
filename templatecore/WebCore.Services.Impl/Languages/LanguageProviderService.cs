using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Globalization;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Languages;
using WebCore.Services.Share.Languages.Dto;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.Languages
{
    public class LanguageProviderService : BaseService, ILanguageProviderService
    {
        private readonly IRepository<LanguageDetail, int> languageDetailRepository;
        private readonly IRepository<Language, int> languageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;
        public LanguageProviderService(IRepository<LanguageDetail, int> languageDetailRepository
            , IUnitOfWork unitOfWork
            , IMapper mapper
            , IMemoryCache memoryCache
            , IRepository<Language, int> languageRepository
            , IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            this.languageDetailRepository = languageDetailRepository;
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
            this.mapper = mapper;
            this.languageRepository = languageRepository;
        }

        public string GetlangByKey(string key)
        {
            // fix key
            if (!key.StartsWith("LBL_"))
            {
                key = "LBL_" + key.ToUpper();
            }
            else
            {
                key = key.ToUpper();
            }

            string langCode = CultureInfo.CurrentCulture.Name;
            string langValue = "[" + key + "]";
            ListResult<LanguageDetailDto> langsInCache = GetLanguageInCache();
            LanguageDetailDto langDetailDto = langsInCache.GetFirstByCondition(x => x.LanguageCode == langCode && x.LanguageKey == key);
            if (langDetailDto == null)
            {
                string[] allLangCodes = languageRepository.GetAll().Select(x => x.LangCode).ToArray();
                foreach (string code in allLangCodes)
                {
                    LanguageDetail langDetail = new LanguageDetail()
                    {
                        LanguageCode = code,
                        LanguageKey = key,
                        LanguageValue = langValue,
                        CreatedBy = GetCurrentUserLogin(),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    langDetail = languageDetailRepository.Add(langDetail);

                    if (code.Equals(langCode))
                    {
                        langDetailDto = mapper.Map<LanguageDetailDto>(langDetail);
                    }
                }

                unitOfWork.SaveChanges();
                AddLanguageToCache(langDetailDto);
            }
            return langDetailDto.LanguageValue;
        }

        public void UpdateLanguage(string code, string key, string value)
        {
            ListResult<LanguageDetailDto> langsInCache = GetLanguageInCache();
            LanguageDetail langDetail = languageDetailRepository.GetFirstByCondition(x => x.LanguageCode == code && x.LanguageKey == key);
            if (langDetail != null)
            {
                langDetail.LanguageValue = value;
                langDetail.UpdateToken = Guid.NewGuid();
                langDetail.ModifiedDate = DateTime.Now;
                langDetail.ModifiedBy = GetCurrentUserLogin();
                languageDetailRepository.Update(langDetail);
                unitOfWork.SaveChanges();
                LanguageDetailDto langDetailDto = mapper.Map<LanguageDetailDto>(langDetail);
                AddLanguageToCache(langDetailDto);
            }
        }


        private ListResult<LanguageDetailDto> GetLanguageInCache()
        {
            ListResult<LanguageDetailDto> languageInCache = memoryCache.Get<ListResult<LanguageDetailDto>>(ConstantConfig.MemoryCacheConfig.LanguageCache);
            string currentCode = CultureInfo.CurrentCulture.Name;
            if (languageInCache == null || (languageInCache.DataList.Count == 0 || !languageInCache.DataList.First().LanguageCode.Equals(currentCode)))
            {
                System.Linq.IQueryable<LanguageDetailDto> dtoQuery = languageDetailRepository
                    .GetByCondition(x => x.LanguageCode == currentCode)
                    .ProjectTo<LanguageDetailDto>(mapper.ConfigurationProvider);
                languageInCache = dtoQuery.ToListResult();
                memoryCache.Set(ConstantConfig.MemoryCacheConfig.LanguageCache, languageInCache);
            }
            return languageInCache;
        }
        private void AddLanguageToCache(LanguageDetailDto languageDetailDto)
        {
            if (languageDetailDto.LanguageCode == CultureInfo.CurrentCulture.Name)
            {
                ListResult<LanguageDetailDto> languagesInCache = GetLanguageInCache();
                LanguageDetailDto lang = languagesInCache.DataList.Where(x => x.LanguageKey == languageDetailDto.LanguageKey).FirstOrDefault();
                if (lang == null)
                {
                    languagesInCache.DataList.Add(languageDetailDto);
                    memoryCache.Set(ConstantConfig.MemoryCacheConfig.LanguageCache, languagesInCache);
                }
                else
                {
                    lang.LanguageValue = languageDetailDto.LanguageValue;
                }
            }
        }
        public ListResult<LanguageDetailDto> GetLanguagesByCode(string code)
        {
            ListResult<LanguageDetailDto> langsInCache = GetLanguageInCache();
            return
                new ListResult<LanguageDetailDto>()
                {
                    DataList = langsInCache.GetByCondition(x => x.LanguageCode.ToLower().Equals(code.ToLower()))
                };
        }


    }
}
