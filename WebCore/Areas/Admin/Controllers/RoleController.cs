using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models.Roles;
using WebCore.Entities;
using WebCore.Services.Share.Admins.Roles;
using WebCore.Services.Share.Admins.Roles.Dto;
using WebCore.Services.Share.Permissions;
using WebCore.Utils.Attributes;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ClaimRequirement(ConstantConfig.Claims.UserManagement_AssignPermission)]
    public class RoleController : AdminBaseController
    {
        private readonly IRoleService roleService;
        private readonly IPermissionService permissionService;

        public RoleController(IServiceProvider serviceProvider, IRoleService roleService, IPermissionService permissionService) : base(serviceProvider)
        {
            this.roleService = roleService;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int pageIndex = 1)
        {
            RoleFilterInput filterInput = GetFilterInSession<RoleFilterInput>(ConstantConfig.SessionName.RoleSession);
            filterInput.PageNumber = pageIndex;
            RoleViewModel roleViewModel = new RoleViewModel
            {
                FilterInput = filterInput,
                PagingResult = roleService.GetAllByPaging(filterInput)
            };
            InitAdminBaseViewModel(roleViewModel);

            return View(roleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterPartial(RoleFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.RoleSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public IActionResult InputPartial(EntityId<string> idModel = null)
        {
            RoleInput input = null;
            if (idModel == null)
            {
                input = new RoleInput();
            }
            else
            {
                input = roleService.GetInputById(idModel);
            }
            return PartialView(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InputPartial(RoleInput inputModel)
        {
            if (inputModel.Id != null)
            {
                //update
                WebCoreRole lastInfo = roleService.GetById(inputModel);
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(inputModel.UpdateToken))
                {
                    await roleService.UpdateInfoAsync(inputModel);
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
                }
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
            }
            return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = GetLang(ConstantConfig.WebApiResultMessage.Error) });
        }

        [HttpGet]
        public async Task<IActionResult> AssignPermissionPartial(EntityId<string> idModel = null)
        {
            WebCoreRole role = roleService.GetById(idModel);
            if (role == null)
            {
                return Forbid();
            }
            string[] allClaims = await roleService.GetAllClaimsAsync(idModel);
            Services.Share.Permissions.Dto.PermissionDto treeViewPermission = await permissionService.GetPermissionTreeViewAsync(allClaims);
            ViewBag.RoleId = idModel.Id;
            return PartialView(treeViewPermission);
        }

        [HttpPost]
        public async Task<IActionResult> AssignPermissionPartial(EntityId<string> roleId, List<string> permissions)
        {
            try
            {
                // Kiểm tra trong danh sách quyền user gửi lên nếu có quyền không có trong danh sách quyền hệ thống thì thông báo lỗi
                HashSet<string> allPermissions = permissionService.GetAllPermissions();
                if (permissions.Any(x => allPermissions.Count(a => a.ToLower() == x.ToLower()) == 0))
                {
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = GetLang(ConstantConfig.WebApiResultMessage.Error) });
                }

                await roleService.UpdateClaimsAsync(roleId, permissions);

                return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.Success) });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult MainListPartial()
        {
            RoleFilterInput filterInput = GetFilterInSession<RoleFilterInput>(ConstantConfig.SessionName.RoleSession);
            PagingResultDto<RoleDto> pagingResult = roleService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }
    }
}