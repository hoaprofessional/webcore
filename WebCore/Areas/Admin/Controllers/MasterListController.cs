﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(int page = 0, string group = null)
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
            filterInput.Group = group;
            filterInput.PageNumber = page;
            viewModel.MainListResult = masterListAdminService.GetAllByPaging(filterInput);
            ViewData["group"] = group;
            viewModel.MasterListFilterInput = filterInput;
            InitAdminBaseViewModel(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MainListPartial(string group = null)
        {
            MasterListFilterInput filterInput = GetFilterInSession<MasterListFilterInput>(ConstantConfig.SessionName.MasterListSession);
            filterInput.Group = group;
            ViewData["group"] = group;
            PagingResultDto<MasterListDto> pagingResult = masterListAdminService.GetAllByPaging(filterInput);
            return PartialView(pagingResult);
        }

        [HttpGet]
        public IActionResult SaveSorting(string sorting, string group = null)
        {
            MasterListFilterInput filterInput = GetFilterInSession<MasterListFilterInput>(ConstantConfig.SessionName.MasterListSession);
            filterInput.Sorting = sorting;
            SetFilterToSession(ConstantConfig.SessionName.MasterListSession, filterInput);
            return RedirectToAction("MainListPartial", new { group = group });
        }

        [HttpGet]
        public IActionResult FilterPartial(MasterListFilterInput filterInput)
        {
            SetFilterToSession(ConstantConfig.SessionName.MasterListSession, filterInput);
            return RedirectToAction("Index", new { page = 1, group = filterInput.Group });
        }

        [HttpGet]
        public async Task<IActionResult> InputPartial(EntityId<int> idModel, string group = null)
        {
            // init input model
            MasterListInput input = masterListAdminService.GetInputById(idModel);
            if (input == null)
            {
                if (!HasPermission(ConstantConfig.Claims.MasterListManagement_AddMasterList))
                {
                    return Forbid();
                }
                input = new MasterListInput();
            }
            else
            {
                if (!HasPermission(ConstantConfig.Claims.MasterListManagement_ActionButton_UpdateMasterList))
                {
                    return Forbid();
                }
            }
            input.Group = group;
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
                    if (!HasPermission(ConstantConfig.Claims.MasterListManagement_ActionButton_UpdateMasterList))
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
                    if (!HasPermission(ConstantConfig.Claims.MasterListManagement_AddMasterList))
                    {
                        return Forbid();
                    }
                    MasterListInput result = masterListAdminService.Add(inputModel);
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
            MasterList lastInfo = masterListAdminService.GetById(deleteInput);
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
        [ClaimRequirement(ConstantConfig.Claims.MasterListManagement_RestoreMasterList)]
        public IActionResult RestoreModel(UpdateTokenModel<int> deleteInput)
        {
            MasterList lastInfo = masterListAdminService.GetById(deleteInput);
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