using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebCore.Areas.Admin.Models;
using WebCore.Services.Share.AdminMenus;
using WebCore.Services.Share.Admins.Languages;
using WebCore.Services.Share.Admins.Languages.Dto;
using WebCore.Services.Share.Languages;
using WebCore.Services.Share.RecordStatuss;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        protected IServiceProvider serviceProvider;
        private readonly ILanguageProviderService languageProvider;
        private readonly ILanguageAdminService languageAdminService;
        private readonly IRecordStatusHelper recordStatusHelper;
        private readonly IAdminMenuProvider adminMenuProvider;

        public AdminBaseController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            languageProvider = (ILanguageProviderService)serviceProvider.GetService(typeof(ILanguageProviderService));
            languageAdminService = (ILanguageAdminService)serviceProvider.GetService(typeof(ILanguageAdminService));
            recordStatusHelper = (IRecordStatusHelper)serviceProvider.GetService(typeof(IRecordStatusHelper));
            adminMenuProvider = (IAdminMenuProvider)serviceProvider.GetService(typeof(IAdminMenuProvider));
        }

        protected void InitAdminBaseViewModel(AdminBaseViewModel adminBaseViewModel)
        {
            var permissions = GetAllPermissions();
            adminBaseViewModel.CurrentLanguage = CultureInfo.CurrentCulture.Name;
            List<LanguageDto> languages = languageAdminService.GetAllLanguages();
            SelectList languagesSelectList = new SelectList(languages, nameof(LanguageDto.LangCode), nameof(LanguageDto.LangName));
            ViewBag.Languages = languagesSelectList;
            ViewBag.RecordStatusCombobox = recordStatusHelper.GetRecordStatusCombobox();
            string currentLink = Request.Path.ToString();
            adminBaseViewModel.Menus = adminMenuProvider.GetAdminMenuTreeView(permissions, currentLink);
        }

        protected string[] GetAllPermissions()
        {
            if (HttpContext.User == null)
            {
                return new string[0];
            }
            return HttpContext.User.Claims.Where(x => x.Type == ConstantConfig.ClaimType.Permission).Select(x => x.Value).ToArray();
        }

        protected TFilter GetFilterInSession<TFilter>(string sessionName) where TFilter : new()
        {
            try
            {
                string filter = HttpContext.Session.GetString(sessionName);
                if (filter == null)
                {
                    TFilter filterModel = new TFilter();
                    return filterModel;
                }
                else
                {
                    return JsonConvert.DeserializeObject<TFilter>(filter);
                }
            }
            catch
            {
                return new TFilter();
            }
        }

        protected IEnumerable<ModelStateErrorModel> GetModelErrors()
        {
            return ModelState
                .Where(x => x.Value.Errors.Any())
                .Select(x => new ModelStateErrorModel()
                {
                    PropertyName = x.Key,
                    ErrorMessages = x.Value.Errors.Select(e => e.ErrorMessage)
                });
        }

        protected void SetFilterToSession(string sessionName, object filterObject)
        {
            string jsonString = JsonConvert.SerializeObject(filterObject);
            HttpContext.Session.SetString(sessionName, jsonString);
        }

        protected bool HasPermission(string permission)
        {
            return HttpContext.User.Claims.Any(x => x.Value == permission);
        }

        protected string GetLang(string code)
        {
            return languageProvider.GetlangByKey(code);
        }
    }
}