using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using WebCore.Areas.Admin.Models;
using WebCore.EntityFramework.Helper;
using WebCore.Services.Share.Admins.LanguageDetails;
using WebCore.Services.Share.Admins.LanguageDetails.Dto;
using WebCore.Services.Share.Languages;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguageDetailController : AdminBaseController
    {
        private readonly ILanguageDetailAdminService languageDetailService;
        private readonly ILanguageProviderService languageProviderService;
        private readonly IUnitOfWork unitOfWork;
        public LanguageDetailController(IServiceProvider serviceProvider, ILanguageProviderService languageProviderService, IUnitOfWork unitOfWork, ILanguageDetailAdminService languageDetailService)
            : base(serviceProvider)
        {
            this.languageDetailService = languageDetailService;
            this.languageProviderService = languageProviderService;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(int page = 0, string langCode = "")
        {
            if (string.IsNullOrEmpty(langCode))
            {
                langCode = CultureInfo.CurrentCulture.Name;
            }
            LanguageDetailViewModel viewModel = new LanguageDetailViewModel();
            LanguageDetailFilterInput filterInput = GetFilterInSession<LanguageDetailFilterInput>(ConstantConfig.SessionName.LanguageDetailSession);
            filterInput.PageNumber = page;
            filterInput.LangCode = langCode;

            viewModel.PagingResult = languageDetailService.GetAllByPaging(filterInput);
            viewModel.LangCode = langCode;
            viewModel.FilterInput = filterInput;
            ViewData["langCode"] = langCode;
            SetFilterToSession(ConstantConfig.SessionName.LanguageDetailSession, filterInput);
            InitAdminBaseViewModel(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult InputPartial(EntityId<int> idModel = null)
        {
            LanguageDetailInput input = null;
            if (idModel == null)
            {
                input = new LanguageDetailInput();
            }
            else
            {
                input = languageDetailService.GetInputById(idModel);
            }

            return PartialView(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InputPartial(LanguageDetailInput inputModel)
        {
            if (inputModel.Id > 0)
            {
                //update
                Entities.LanguageDetail lastInfo = languageDetailService.GetById(inputModel);
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(inputModel.UpdateToken))
                {
                    languageProviderService.UpdateLanguage(lastInfo.LanguageCode, lastInfo.LanguageKey, inputModel.LanguageValue);
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
                }
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
            }
            return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = GetLang(ConstantConfig.WebApiResultMessage.Error) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterPartial(LanguageDetailFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.LanguageDetailSession, filterInput);
            return RedirectToAction("Index", new { page = 1, langCode = filterInput.LangCode });
        }

        public IActionResult MainListPartial()
        {
            LanguageDetailFilterInput filterInput = GetFilterInSession<LanguageDetailFilterInput>(ConstantConfig.SessionName.LanguageDetailSession);
            ViewData["langCode"] = filterInput.LangCode;
            PagingResultDto<LanguageDetailDto> pagingResult = languageDetailService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }
    }
}