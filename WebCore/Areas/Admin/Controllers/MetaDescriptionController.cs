using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models.MetaDescriptions;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.Services.Share.Admins.MetaDescriptions;
using WebCore.Services.Share.Admins.MetaDescriptions.Dto;
using WebCore.Services.Share.Permissions;
using WebCore.Utils.Attributes;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ClaimRequirement(ConstantConfig.Claims.MetaDescriptionManagement)]
    public class MetaDescriptionController : AdminBaseController
    {
        private readonly IMetaDescriptionAdminService masterListAdminService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public MetaDescriptionController(IServiceProvider serviceProvider,
            IPermissionService permissionService,
            IMetaDescriptionAdminService masterListAdminService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.masterListAdminService = masterListAdminService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int page = 0)
        {
            MetaDescriptionViewModel viewModel = new MetaDescriptionViewModel();
            MetaDescriptionFilterInput filterInput = GetFilterInSession<MetaDescriptionFilterInput>(ConstantConfig.SessionName.MetaDescriptionSession);
            if (filterInput == null)
            {
                filterInput = new MetaDescriptionFilterInput
                {
                    RecordStatus = ConstantConfig.RecordStatusConfig.Active
                };
            }
            filterInput.PageNumber = page;
            viewModel.MainListResult = masterListAdminService.GetAllByPaging(filterInput);
            viewModel.MetaDescriptionFilterInput = filterInput;
            InitAdminBaseViewModel(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MainListPartial()
        {
            MetaDescriptionFilterInput filterInput = GetFilterInSession<MetaDescriptionFilterInput>(ConstantConfig.SessionName.MetaDescriptionSession);
            PagingResultDto<MetaDescriptionDto> pagingResult = masterListAdminService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }

        [HttpGet]
        public IActionResult SaveSorting(string sorting)
        {
            MetaDescriptionFilterInput filterInput = GetFilterInSession<MetaDescriptionFilterInput>(ConstantConfig.SessionName.MetaDescriptionSession);
            filterInput.Sorting = sorting;
            SetFilterToSession(ConstantConfig.SessionName.MetaDescriptionSession, filterInput);
            return RedirectToAction("MainListPartial");
        }

        [HttpGet]
        public IActionResult FilterPartial(MetaDescriptionFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.MetaDescriptionSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public IActionResult InputPartial(EntityId<int> idModel)
        {
            // init input model
            MetaDescriptionInput input = masterListAdminService.GetInputById(idModel);
            if (input == null)
            {
                if (!HasPermission(ConstantConfig.Claims.MetaDescriptionManagement_AddMetaDescription))
                {
                    return Forbid();
                }
                input = new MetaDescriptionInput();
            }
            else
            {
                if (!HasPermission(ConstantConfig.Claims.MetaDescriptionManagement_ActionButton_UpdateMetaDescription))
                {
                    return Forbid();
                }
            }
            // init combobox
            return PartialView(input);
        }
        [HttpPost]
        public IActionResult InputPartial([Required]MetaDescriptionInput inputModel)
        {
            try
            {
                MetaDescription lastInfo = masterListAdminService.GetById(inputModel);
                if (lastInfo != null)
                {
                    // update
                    if (!HasPermission(ConstantConfig.Claims.MetaDescriptionManagement_ActionButton_UpdateMetaDescription))
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
                    if (!HasPermission(ConstantConfig.Claims.MetaDescriptionManagement_AddMetaDescription))
                    {
                        return Forbid();
                    }
                    MetaDescriptionInput result = masterListAdminService.Add(inputModel);
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
        [ClaimRequirement(ConstantConfig.Claims.MetaDescriptionManagement_ActionButton_DeleteMetaDescription)]
        public IActionResult DeleteModel(UpdateTokenModel<int> deleteInput)
        {
            MetaDescription lastInfo = masterListAdminService.GetById(deleteInput);
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
        [ClaimRequirement(ConstantConfig.Claims.MetaDescriptionManagement_RestoreMetaDescription)]
        public IActionResult RestoreModel(UpdateTokenModel<int> deleteInput)
        {
            MetaDescription lastInfo = masterListAdminService.GetById(deleteInput);
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