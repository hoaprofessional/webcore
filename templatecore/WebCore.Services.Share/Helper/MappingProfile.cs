using AutoMapper;
using WebCore.Entities;
using WebCore.Services.Share.Admins.Languages.Dto;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Services.Share.Languages.Dto;
using WebCore.Services.Share.SystemConfigs.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LanguageDetail, Languages.Dto.LanguageDetailDto>();
            CreateMap<ListResult<LanguageDetail>, ListResult<Languages.Dto.LanguageDetailDto>>();

            CreateMap<SystemConfig, SystemConfigDto>();
            CreateMap<ListResult<SystemConfig>, ListResult<SystemConfigDto>>();


            CreateMap<LanguageDetail, LanguageInsertUpdateInput>();
            CreateMap<LanguageInsertUpdateInput, LanguageDetail>();


            CreateMap<WebCoreUser, UserDto>();
            CreateMap<WebCoreUser, WebCore.Services.Share.Admins.Users.Dto.UserInfoInput>();
            CreateMap<WebCore.Services.Share.Admins.Users.Dto.UserInfoInput, WebCoreUser>();
            CreateMap<WebCoreUser, WebCore.Services.Share.Admins.Users.Dto.UserResetPasswordInput>();
            CreateMap<WebCore.Services.Share.Admins.Users.Dto.UserResetPasswordInput, WebCoreUser>();

            CreateMap<WebCoreRole, Admins.Roles.Dto.RoleDto>();
            CreateMap<WebCoreRole, WebCore.Services.Share.Admins.Roles.Dto.RoleInput>();
            CreateMap<WebCore.Services.Share.Admins.Roles.Dto.RoleInput, WebCoreRole>();

            CreateMap<Language, LanguageDto>();

            CreateMap<LanguageDetail, WebCore.Services.Share.Admins.LanguageDetails.Dto.LanguageDetailDto>();
            CreateMap<LanguageDetail, WebCore.Services.Share.Admins.LanguageDetails.Dto.LanguageDetailInput>();
            CreateMap<WebCore.Services.Share.Admins.LanguageDetails.Dto.LanguageDetailInput, LanguageDetail>();

            CreateMap<AdminMenu, Admins.AdminMenus.Dto.AdminMenuDto>();
            CreateMap<AdminMenu, WebCore.Services.Share.Admins.AdminMenus.Dto.AdminMenuInput>();
            CreateMap<WebCore.Services.Share.Admins.AdminMenus.Dto.AdminMenuInput, AdminMenu>();

            CreateMap<MasterList, Admins.MasterListGroups.Dto.MasterListGroupDto>();
            CreateMap<MasterList, WebCore.Services.Share.Admins.MasterListGroups.Dto.MasterListGroupInput>();
            CreateMap<WebCore.Services.Share.Admins.MasterListGroups.Dto.MasterListGroupInput, MasterList>();

            CreateMap<MasterList, Admins.MasterLists.Dto.MasterListDto>();
            CreateMap<MasterList, WebCore.Services.Share.Admins.MasterLists.Dto.MasterListInput>();
            CreateMap<WebCore.Services.Share.Admins.MasterLists.Dto.MasterListInput, MasterList>();

            CreateMap<AdminMenu, AdminMenus.Dto.AdminMenuTreeViewDto>();


        }
    }
}
