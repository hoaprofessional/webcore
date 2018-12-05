using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models.AdminMenus;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.Services.Share.Admins.AdminMenus;
using WebCore.Services.Share.Admins.AdminMenus.Dto;
using WebCore.Services.Share.Permissions;
using WebCore.Utils.Attributes;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ClaimRequirement(ConstantConfig.Claims.AdminMenuManagement)]
    public class AdminMenuController : AdminBaseController
    {
        private readonly IAdminMenuAdminService adminMenuAdminService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public AdminMenuController(IServiceProvider serviceProvider,
            IPermissionService permissionService,
            IAdminMenuAdminService adminMenuAdminService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.adminMenuAdminService = adminMenuAdminService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int page = 0)
        {
            AdminMenuViewModel viewModel = new AdminMenuViewModel();
            AdminMenuFilterInput filterInput = GetFilterInSession<AdminMenuFilterInput>(ConstantConfig.SessionName.AdminMenuSession);
            if (filterInput == null)
            {
                filterInput = new AdminMenuFilterInput
                {
                    RecordStatus = ConstantConfig.RecordStatusConfig.Active
                };
            }
            filterInput.PageNumber = page;
            viewModel.PagingResult = adminMenuAdminService.GetAllByPaging(filterInput);
            viewModel.AdminMenuFilterInput = filterInput;
            ViewBag.AdminMenuCombobox = adminMenuAdminService.GetAdminMenusCombobox();

            InitAdminBaseViewModel(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MainListPartial()
        {
            AdminMenuFilterInput filterInput = GetFilterInSession<AdminMenuFilterInput>(ConstantConfig.SessionName.AdminMenuSession);
            PagingResultDto<AdminMenuDto> pagingResult = adminMenuAdminService.GetAllByPaging(filterInput);
            ViewBag.AdminMenuCombobox = adminMenuAdminService.GetAdminMenusCombobox();
            return PartialView(pagingResult);
        }

        [HttpGet]
        public IActionResult FilterPartial(AdminMenuFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.AdminMenuSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> InputPartial(EntityId<int> idModel)
        {
            // init input model
            AdminMenuInput input = adminMenuAdminService.GetInputById(idModel);
            if (input == null)
            {
                if (!HasPermission(ConstantConfig.Claims.AdminMenuManagement_AddMenuManagement))
                {
                    return Forbid();
                }
                input = new AdminMenuInput();
            }
            else
            {
                if (!HasPermission(ConstantConfig.Claims.AdminMenuManagement_ActionButton_UpdateMenuManagement))
                {
                    return Forbid();
                }
            }
            // init combobox
            ViewBag.PermissionCombobox = await permissionService.GetPermissionCombobox();
            ViewBag.AdminMenuCombobox = adminMenuAdminService.GetAdminMenusCombobox();
            return PartialView(input);
        }
        [HttpPost]
        public IActionResult InputPartial([Required]AdminMenuInput inputModel)
        {
            try
            {
                AdminMenu lastInfo = adminMenuAdminService.GetById(inputModel);
                if (lastInfo != null)
                {
                    // update
                    if (!HasPermission(ConstantConfig.Claims.AdminMenuManagement_ActionButton_UpdateMenuManagement))
                    {
                        return Forbid();
                    }
                    if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(inputModel.UpdateToken))
                    {
                        adminMenuAdminService.UpdateAdminmenu(inputModel);
                        unitOfWork.SaveChanges();
                        return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
                    }
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
                }
                else
                {
                    // insert
                    if (!HasPermission(ConstantConfig.Claims.AdminMenuManagement_AddMenuManagement))
                    {
                        return Forbid();
                    }
                    AdminMenuInput result = adminMenuAdminService.AddAdminMenu(inputModel);
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
        [ClaimRequirement(ConstantConfig.Claims.AdminMenuManagement_ActionButton_DeleteMenuManagement)]
        public IActionResult DeleteModel(UpdateTokenModel<int> deleteInput)
        {
            AdminMenu lastInfo = adminMenuAdminService.GetById(deleteInput);
            if (lastInfo != null)
            {
                // update
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(deleteInput.UpdateToken))
                {

                    adminMenuAdminService.DeleteAdminMenu(deleteInput);
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
        [ClaimRequirement(ConstantConfig.Claims.AdminMenuManagement_RestoreMenuManagement)]
        public IActionResult RestoreModel(UpdateTokenModel<int> deleteInput)
        {
            AdminMenu lastInfo = adminMenuAdminService.GetById(deleteInput);
            if (lastInfo != null)
            {
                // update
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(deleteInput.UpdateToken))
                {
                    adminMenuAdminService.RestoreAdminMenu(deleteInput);
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