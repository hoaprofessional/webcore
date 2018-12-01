using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebCore.Areas.Admin.Models.Languages;
using WebCore.Services.Share.Admins.Languages;
using WebCore.Services.Share.Admins.Languages.Dto;
using WebCore.Utils.Config;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguageController : AdminBaseController
    {
        private readonly ILanguageAdminService languageAdminService;

        public LanguageController(IServiceProvider serviceProvider, ILanguageAdminService languageAdminService) : base(serviceProvider)
        {
            this.languageAdminService = languageAdminService;
        }

        public IActionResult Index(int page = 0)
        {
            LanguageViewModel viewModel = new LanguageViewModel();

            LanguageFilterInput filterInput = GetFilterInSession<LanguageFilterInput>(ConstantConfig.SessionName.LanguageSession);
            filterInput.PageNumber = page;

            viewModel.PagingResult = languageAdminService.GetAllByPaging(filterInput);
            viewModel.LanguageFilterInput = filterInput;

            InitAdminBaseViewModel(viewModel);

            return View(viewModel);
        }

        public IActionResult SaveFilter(LanguageFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.LanguageSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

    }
}