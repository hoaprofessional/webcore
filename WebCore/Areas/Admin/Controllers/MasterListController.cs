using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models.MasterLists;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.Services.Share.Admins.MasterLists;
using WebCore.Services.Share.Admins.MasterLists.Dto;
using WebCore.Services.Share.Permissions;
using WebCore.Utils.Attributes;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ClaimRequirement(ConstantConfig.Claims.MasterListManagement)]
    public class MasterListController : AdminBaseController
    {
        private readonly IMasterListAdminService masterListAdminService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public MasterListController(IServiceProvider serviceProvider,
            IPermissionService permissionService,
            IMasterListAdminService masterListAdminService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.masterListAdminService = masterListAdminService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int page = 0)
        {
            MasterListViewModel viewModel = new MasterListViewModel();
            MasterListFilterInput filterInput = GetFilterInSession<MasterListFilterInput>(ConstantConfig.SessionName.MasterListSession);
            if (filterInput == null)
            {
                filterInput = new MasterListFilterInput
                {
                    RecordStatus = ConstantConfig.RecordStatusConfig.Active
                };
            }
            filterInput.PageNumber = page;
            viewModel.PagingResult = masterListAdminService.GetAllByPaging(filterInput);
            viewModel.MasterListFilterInput = filterInput;

            InitAdminBaseViewModel(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MainListPartial()
        {
            MasterListFilterInput filterInput = GetFilterInSession<MasterListFilterInput>(ConstantConfig.SessionName.MasterListSession);
            PagingResultDto<MasterListDto> pagingResult = masterListAdminService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }

        [HttpGet]
        public IActionResult FilterPartial(MasterListFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.MasterListSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> InputPartial(EntityId<int> idModel)
        {
            // init input model
            MasterListInput input = masterListAdminService.GetInputById(idModel);
            if (input == null)
            {
                if (!HasPermission(ConstantConfig.Claims.MasterListManagement_AddMenuManagement))
                {
                    return Forbid();
                }
                input = new MasterListInput();
            }
            else
            {
                if (!HasPermission(ConstantConfig.Claims.MasterListManagement_ActionButton_UpdateMenuManagement))
                {
                    return Forbid();
                }
            }
            // init combobox
            ViewBag.PermissionCombobox = await permissionService.GetPermissionCombobox();
            return PartialView(input);
        }
        [HttpPost]
        public IActionResult InputPartial([Required]MasterListInput inputModel)
        {
            try
            {
                MasterList lastInfo = masterListAdminService.GetById(inputModel);
                if (lastInfo != null)
                {
                    // update
                    if (!HasPermission(ConstantConfig.Claims.MasterListManagement_ActionButton_UpdateMenuManagement))
                    {
                        return Forbid();
                    }
                    if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(inputModel.UpdateToken))
                    {
                        masterListAdminService.UpdateAdminmenu(inputModel);
                        unitOfWork.SaveChanges();
                        return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
                    }
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
                }
                else
                {
                    // insert
                    if (!HasPermission(ConstantConfig.Claims.MasterListManagement_AddMenuManagement))
                    {
                        return Forbid();
                    }
                    MasterListInput result = masterListAdminService.AddMasterList(inputModel);
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
        [ClaimRequirement(ConstantConfig.Claims.MasterListManagement_ActionButton_DeleteMenuManagement)]
        public IActionResult DeleteModel(UpdateTokenModel<int> deleteInput)
        {
            MasterList lastInfo = masterListAdminService.GetById(deleteInput);
            if (lastInfo != null)
            {
                // update
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(deleteInput.UpdateToken))
                {

                    masterListAdminService.DeleteMasterList(deleteInput);
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
        [ClaimRequirement(ConstantConfig.Claims.MasterListManagement_RestoreMenuManagement)]
        public IActionResult RestoreModel(UpdateTokenModel<int> deleteInput)
        {
            MasterList lastInfo = masterListAdminService.GetById(deleteInput);
            if (lastInfo != null)
            {
                // update
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(deleteInput.UpdateToken))
                {
                    masterListAdminService.RestoreMasterList(deleteInput);
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