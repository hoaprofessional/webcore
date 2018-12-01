using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models.Users;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.Services.Share.Admins.Users;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Services.Share.Permissions;
using WebCore.Utils.Attributes;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : AdminBaseController
    {
        private readonly IUserService userService;
        private readonly IPermissionService permissionService;
        private readonly IUnitOfWork unitOfWork;

        public UserController(IServiceProvider serviceProvider, IUserService userService, IPermissionService permissionService, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.permissionService = permissionService;
        }

        public IActionResult Index(int pageIndex = 1)
        {
            UserFilterInput filterInput = GetFilterInSession<UserFilterInput>(ConstantConfig.SessionName.UserSession);
            filterInput.PageNumber = pageIndex;
            UserViewModel userViewModel = new UserViewModel
            {
                FilterInput = filterInput,
                PagingResult = userService.GetAllByPaging(filterInput)
            };
            InitAdminBaseViewModel(userViewModel);
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterPartial(UserFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.UserSession, filterInput);
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public IActionResult ResetPasswordPartial(EntityId<string> idModel = null)
        {
            UserResetPasswordInput input = null;
            if (idModel == null)
            {
                input = new UserResetPasswordInput();
            }
            else
            {
                input = userService.GetResetPasswordInputById(idModel);
            }
            return PartialView(input);
        }

        [HttpGet]
        public async Task<IActionResult> AssignPermissionPartial(EntityId<string> idModel = null)
        {
            WebCoreUser user = userService.GetById(idModel);
            UserAssignPermissionViewModel viewModel = new UserAssignPermissionViewModel();
            if (user == null)
            {
                return Forbid();
            }
            string[] allClaims = await userService.GetAllClaimsAsync(idModel);

            viewModel.TreeViewPermission = await permissionService.GetPermissionTreeViewAsync(allClaims);
            viewModel.AllRoles = await userService.GetAllRolesAsync(idModel);

            ViewBag.UserId = idModel.Id;

            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignPermissionPartial(AssignPermissionInput assignPermissionInput)
        {
            WebCoreUser user = userService.GetById(new EntityId<string>()
            {
                Id = assignPermissionInput.UserId
            });
            if (user == null)
            {
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = GetLang(ConstantConfig.WebApiResultMessage.Error) });
            }
            bool success = await userService.UpdatePermissionsAsync(assignPermissionInput);
            if(success)
            {
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
            }
            else
            {
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = GetLang(ConstantConfig.WebApiResultMessage.Error) });
            }
        }

        [HttpGet]
        [ClaimRequirement(ConstantConfig.Claims.UserManagement_AssignPermission)]
        public IActionResult InputInfoPartial(EntityId<string> idModel = null)
        {
            UserInfoInput input = null;
            if (idModel == null)
            {
                input = new UserInfoInput();
            }
            else
            {
                input = userService.GetInputById(idModel);
            }

            return PartialView(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InputInfoPartial(UserInfoInput inputModel)
        {
            if (inputModel.Id != null)
            {
                //update
                WebCoreUser lastInfo = userService.GetById(inputModel);
                if (lastInfo.UpdateToken.GetValueOrDefault(Guid.Empty).Equals(inputModel.UpdateToken))
                {
                    await userService.UpdateInfo(inputModel);
                    return Ok(new { result = ConstantConfig.WebApiStatusCode.Success, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateSuccess) });
                }
                return Ok(new { result = ConstantConfig.WebApiStatusCode.Warning, message = GetLang(ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch) });
            }
            return Ok(new { result = ConstantConfig.WebApiStatusCode.Error, message = GetLang(ConstantConfig.WebApiResultMessage.Error) });
        }

        [HttpPost]
        [ClaimRequirement(ConstantConfig.Claims.UserManagement_BlockUser)]
        public async Task<IActionResult> ActiveUser(EntityId<string> userId)
        {
            await userService.SetActiveAsync(userId, ConstantConfig.UserRecordStatus.Active);
            return Ok();
        }

        [HttpPost]
        [ClaimRequirement(ConstantConfig.Claims.UserManagement_BlockUser)]
        public async Task<IActionResult> InActiveUser(EntityId<string> userId)
        {
            await userService.SetActiveAsync(userId, ConstantConfig.UserRecordStatus.InActive);
            return Ok();
        }

        public IActionResult MainListPartial()
        {
            UserFilterInput filterInput = GetFilterInSession<UserFilterInput>(ConstantConfig.SessionName.UserSession);
            PagingResultDto<UserDto> pagingResult = userService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }


    }
}