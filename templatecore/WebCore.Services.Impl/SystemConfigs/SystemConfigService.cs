﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.SystemConfigs;
using WebCore.Services.Share.SystemConfigs.Dto;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.SystemConfigs
{
    public class SystemConfigService : ISystemConfigService
    {
        private readonly IRepository<SystemConfig, int> systemConfigRepository;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceProvider serviceProvider;

        public SystemConfigService(IRepository<SystemConfig, int> systemConfigRepository
            , IMemoryCache memoryCache
            , IMapper mapper
            , IServiceProvider serviceProvider
            , IUnitOfWork unitOfWork)
        {
            this.systemConfigRepository = systemConfigRepository;
            this.memoryCache = memoryCache;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.serviceProvider = serviceProvider;
        }
        private string GetCurrentUserLogin()
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));
            return httpContextAccessor.HttpContext.User?.Identity?.Name;
        }
        public decimal GetValueNumber(string key)
        {
            Thread ct = Thread.CurrentThread;
            List<SystemConfigDto> systemConfigsInCache = GetSystemConfigInCache();
            SystemConfigDto systemConfigDto = systemConfigsInCache.FirstOrDefault(x => x.Key == key);
            if (systemConfigDto == null)
            {
                SystemConfig systemConfig = new SystemConfig()
                {
                    Key = key,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = GetCurrentUserLogin(),
                    ValueNumber = -999999,
                    ValueString = "[]"
                };
                systemConfig = systemConfigRepository.Add(systemConfig);
                unitOfWork.SaveChanges();
                systemConfigDto = mapper.Map<SystemConfigDto>(systemConfig);
                AddSystemConfigToCache(systemConfigDto);
            }
            return systemConfigDto.ValueNumber;
        }

        public string GetValueString(string key)
        {
            List<SystemConfigDto> systemConfigsInCache = GetSystemConfigInCache();
            SystemConfigDto systemConfigDto = systemConfigsInCache.FirstOrDefault(x => x.Key == key);
            if (systemConfigDto == null)
            {
                SystemConfig systemConfig = new SystemConfig()
                {
                    Key = key,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = GetCurrentUserLogin(),
                    ValueNumber = -999999,
                    ValueString = "[]"
                };
                systemConfigRepository.Add(systemConfig);
                unitOfWork.SaveChanges();
                systemConfigDto = mapper.Map<SystemConfigDto>(systemConfig);
                AddSystemConfigToCache(systemConfigDto);
            }
            return systemConfigDto.ValueString;
        }

        private List<SystemConfigDto> GetSystemConfigInCache()
        {
            List<SystemConfigDto> sysConfigDtoInCache = memoryCache.Get<List<SystemConfigDto>>(ConstantConfig.MemoryCacheConfig.SystemConfigCache);
            if (sysConfigDtoInCache == null)
            {
                System.Linq.IQueryable<SystemConfigDto> dtoQuery = systemConfigRepository
                    .GetAll()
                    .ProjectTo<SystemConfigDto>(mapper.ConfigurationProvider);

                sysConfigDtoInCache = dtoQuery.ToList();
                memoryCache.Set(ConstantConfig.MemoryCacheConfig.SystemConfigCache, sysConfigDtoInCache);
            }
            return sysConfigDtoInCache;
        }

        private void AddSystemConfigToCache(SystemConfigDto systemConfigDto)
        {
            List<SystemConfigDto> sysConfigsInCache = GetSystemConfigInCache();
            sysConfigsInCache.Add(systemConfigDto);
            memoryCache.Set(ConstantConfig.MemoryCacheConfig.SystemConfigCache, systemConfigDto);
        }
    }
}
