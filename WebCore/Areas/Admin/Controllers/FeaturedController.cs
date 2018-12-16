using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models.Featureds;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.Services.Share.Admins.Featureds;
using WebCore.Services.Share.Admins.Featureds.Dto;
using WebCore.Services.Share.MasterLists;
using WebCore.Services.Share.Permissions;
using WebCore.Utils.Attributes;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ClaimRequirement(ConstantConfig.Claims.FeaturedManagement)]
    public class FeaturedController : AdminBaseController
    {
        private readonly IFeaturedAdminService masterListAdminService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMasterListProvider masterListProvider;

        public FeaturedController(IServiceProvider serviceProvider,
            IPermissionService permissionService,
            IMasterListProvider masterListProvider,
            IFeaturedAdminService masterListAdminService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.masterListAdminService = masterListAdminService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
            this.masterListProvider = masterListProvider;
        }

        public IActionResult Index(int page = 0)
        {
            FeaturedViewModel viewModel = new FeaturedViewModel();
            FeaturedFilterInput filterInput = GetFilterInSession<FeaturedFilterInput>(ConstantConfig.SessionName.FeaturedSession);
            if (filterInput == null)
            {
                filterInput = new FeaturedFilterInput
                {
                    RecordStatus = ConstantConfig.RecordStatusConfig.Active
                };
            }
            filterInput.PageNumber = page;
            viewModel.MainListResult = masterListAdminService.GetAllByPaging(filterInput);
            viewModel.FeaturedFilterInput = filterInput;
            InitAdminBaseViewModel(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MainListPartial()
        {
            FeaturedFilterInput filterInput = GetFilterInSession<FeaturedFilterInput>(ConstantConfig.SessionName.FeaturedSession);
            PagingResultDto<FeaturedDto> pagingResult = masterListAdminService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }

        [HttpGet]
        public IActionResult SaveSorting(string sorting)
        {
            FeaturedFilterInput filterInput = GetFilterInSession<FeaturedFilterInput>(ConstantConfig.SessionName.FeaturedSession);
            filterInput.Sorting = sorting;
            SetFilterToSession(ConstantConfig.SessionName.FeaturedSession, filterInput);
            return RedirectToAction("MainListPartial");
        }

        [HttpGet]
        public IActionResult FilterPartial(FeaturedFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.FeaturedSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public IActionResult InputPartial(EntityId<int> idModel)
        {
            // init input model
            FeaturedInput input = masterListAdminService.GetInputById(idModel);
            if (input == null)
            {
                if (!HasPermission(ConstantConfig.Claims.FeaturedManagement_AddFeatured))
                {
                    return Forbid();
                }
                input = new FeaturedInput();
                input.TextArea = "<b>bold</b>";
            }
            else
            {
                if (!HasPermission(ConstantConfig.Claims.FeaturedManagement_ActionButton_UpdateFeatured))
                {
                    return Forbid();
                }
            }
            // init combobox
            ViewBag.FeaturedCombobox = masterListProvider.SelectItemByGroup(ConstantConfig.MasterListGroup.FeaturedCombobox);
            return PartialView(input);
        }
        [HttpPost]
        public IActionResult InputPartial([Required]FeaturedInput inputModel)
        {
            try
            {
                Featured lastInfo = masterListAdminService.GetById(inputModel);
                if (lastInfo != null)
                {
                    // update
                    if (!HasPermission(ConstantConfig.Claims.FeaturedManagement_ActionButton_UpdateFeatured))
                    {
                        return Forbid();
                    }
                    if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(inputModel.UpdateToken))
                    {
                        masterListAdminService.Update(inputModel);
                        unitOfWork.SaveChanges();
                        return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
                    }
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
                }
                else
                {
                    // insert
                    if (!HasPermission(ConstantConfig.Claims.FeaturedManagement_AddFeatured))
                    {
                        return Forbid();
                    }
                    FeaturedInput result = masterListAdminService.Add(inputModel);
                    if (result == null)
                    {
                        return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = GetLang(ConstantConfig.WebApiResultMessage.Error) });
                    }
                    unitOfWork.SaveChanges();
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
                }
            }
            catch
            {
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = GetLang(ConstantConfig.WebApiResultMessage.Error) });
            }
        }

        [HttpPost]
        [ClaimRequirement(ConstantConfig.Claims.FeaturedManagement_ActionButton_DeleteFeatured)]
        public IActionResult DeleteModel(UpdateTokenModel<int> deleteInput)
        {
            Featured lastInfo = masterListAdminService.GetById(deleteInput);
            if (lastInfo != null)
            {
                // update
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(deleteInput.UpdateToken))
                {

                    masterListAdminService.Delete(deleteInput);
                    unitOfWork.SaveChanges();
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.DeleteSuccess) });
                }
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        [ClaimRequirement(ConstantConfig.Claims.FeaturedManagement_RestoreFeatured)]
        public IActionResult RestoreModel(UpdateTokenModel<int> deleteInput)
        {
            Featured lastInfo = masterListAdminService.GetById(deleteInput);
            if (lastInfo != null)
            {
                // update
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(deleteInput.UpdateToken))
                {
                    masterListAdminService.Restore(deleteInput);
                    unitOfWork.SaveChanges();
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.RestoreSuccess) });
                }
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
            }
            else
            {
                return Forbid();
            }
        }

    }
}