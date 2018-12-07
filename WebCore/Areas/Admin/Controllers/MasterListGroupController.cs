using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models.MasterListGroups;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.Services.Share.Admins.MasterListGroups;
using WebCore.Services.Share.Admins.MasterListGroups.Dto;
using WebCore.Services.Share.Permissions;
using WebCore.Utils.Attributes;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ClaimRequirement(ConstantConfig.Claims.MasterListManagement)]
    public class MasterListGroupController : AdminBaseController
    {
        private readonly IMasterListGroupAdminService masterListGroupAdminService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public MasterListGroupController(IServiceProvider serviceProvider,
            IPermissionService permissionService,
            IMasterListGroupAdminService masterListGroupAdminService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.masterListGroupAdminService = masterListGroupAdminService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int page = 0)
        {
            MasterListGroupViewModel viewModel = new MasterListGroupViewModel();
            MasterListGroupFilterInput filterInput = GetFilterInSession<MasterListGroupFilterInput>(ConstantConfig.SessionName.MasterListGroupSession);
            if (filterInput == null)
            {
                filterInput = new MasterListGroupFilterInput
                {
                    RecordStatus = ConstantConfig.RecordStatusConfig.Active
                };
            }
            filterInput.PageNumber = page;
            viewModel.PagingResult = masterListGroupAdminService.GetAllByPaging(filterInput);
            viewModel.MasterListGroupFilterInput = filterInput;

            InitAdminBaseViewModel(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MainListPartial()
        {
            MasterListGroupFilterInput filterInput = GetFilterInSession<MasterListGroupFilterInput>(ConstantConfig.SessionName.MasterListGroupSession);
            PagingResultDto<MasterListGroupDto> pagingResult = masterListGroupAdminService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }

        [HttpGet]
        public IActionResult FilterPartial(MasterListGroupFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.MasterListGroupSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> InputPartial(EntityId<int> idModel)
        {
            // init input model
            MasterListGroupInput input = masterListGroupAdminService.GetInputById(idModel);
            if (input == null)
            {
                if (!HasPermission(ConstantConfig.Claims.MasterListManagement_AddMasterList))
                {
                    return Forbid();
                }
                input = new MasterListGroupInput();
            }
            else
            {
                if (!HasPermission(ConstantConfig.Claims.MasterListManagement_ActionButton_UpdateMasterList))
                {
                    return Forbid();
                }
            }
            // init combobox
            ViewBag.PermissionCombobox = await permissionService.GetPermissionCombobox();
            return PartialView(input);
        }
        [HttpPost]
        public IActionResult InputPartial([Required]MasterListGroupInput inputModel)
        {
            try
            {
                MasterList lastInfo = masterListGroupAdminService.GetById(inputModel);
                if (lastInfo != null)
                {
                    // update
                    if (!HasPermission(ConstantConfig.Claims.MasterListManagement_ActionButton_UpdateMasterList))
                    {
                        return Forbid();
                    }
                    if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(inputModel.UpdateToken))
                    {
                        masterListGroupAdminService.Update(inputModel);
                        unitOfWork.SaveChanges();
                        return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
                    }
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
                }
                else
                {
                    // insert
                    if (!HasPermission(ConstantConfig.Claims.MasterListManagement_AddMasterList))
                    {
                        return Forbid();
                    }
                    MasterListGroupInput result = masterListGroupAdminService.Add(inputModel);
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
        [ClaimRequirement(ConstantConfig.Claims.MasterListManagement_ActionButton_DeleteMasterList)]
        public IActionResult DeleteModel(UpdateTokenModel<int> deleteInput)
        {
            MasterList lastInfo = masterListGroupAdminService.GetById(deleteInput);
            if (lastInfo != null)
            {
                // update
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(deleteInput.UpdateToken))
                {

                    masterListGroupAdminService.Delete(deleteInput);
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
        [ClaimRequirement(ConstantConfig.Claims.MasterListManagement_RestoreMasterList)]
        public IActionResult RestoreModel(UpdateTokenModel<int> deleteInput)
        {
            MasterList lastInfo = masterListGroupAdminService.GetById(deleteInput);
            if (lastInfo != null)
            {
                // update
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(deleteInput.UpdateToken))
                {
                    masterListGroupAdminService.Restore(deleteInput);
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