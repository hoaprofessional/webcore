using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Utils.Config
{
    public static class ConstantConfig
    {
        public static class MemoryCacheConfig
        {
            public const string LanguageCache = "LanguageCache";
            public const string LanguageSelectCache = "LanguageSelectCache";
            public const string SystemConfigCache = "SystemConfigCache";
        }
        public static class Claims
        {
            public const string Admin = "Admin";
            public const string LanguagePage = "LanguagePage";
            public const string LanguagePage_AddLanguage = "LanguagePage.AddLanguage";
            public const string LanguagePage_UpdateLanguage = "LanguagePage.UpdateLanguage";
            public const string LanguagePage_DeleteLanguage = "LanguagePage.DeleteLanguage";
            public const string UserManagement = "UserManagement";
            public const string UserManagement_BlockUser = "UserManagement.BlockUser";
            public const string UserManagement_AssignPermission = "UserManagement.AssignPermission";

            public const string AdminMenuManagement = "AdminMenuManagement";
            public const string AdminMenuManagement_AddMenuManagement = "AdminMenuManagement.AddMenuManagement";
            public const string AdminMenuManagement_RestoreMenuManagement = "AdminMenuManagement.RestoreMenuManagement";
            public const string AdminMenuManagement_ActionButton = "AdminMenuManagement.ActionButton";
            public const string AdminMenuManagement_ActionButton_UpdateMenuManagement = "AdminMenuManagement.ActionButton.UpdateMenuManagement";
            public const string AdminMenuManagement_ActionButton_DeleteMenuManagement = "AdminMenuManagement.ActionButton.DeleteMenuManagement";

            public const string MasterListManagement = "MasterListManagement";
            public const string MasterListManagement_AddMenuManagement = "MasterListManagement.AddMenuManagement";
            public const string MasterListManagement_RestoreMenuManagement = "MasterListManagement.RestoreMenuManagement";
            public const string MasterListManagement_ActionButton = "MasterListManagement.ActionButton";
            public const string MasterListManagement_ActionButton_UpdateMenuManagement = "MasterListManagement.ActionButton.UpdateMenuManagement";
            public const string MasterListManagement_ActionButton_DeleteMenuManagement = "MasterListManagement.ActionButton.DeleteMenuManagement";
        }
        public static class WebApiStatusCode
        {
            public const long Success = 0;
            public const long Error = 1;
            public const long Warning = 2;
            public const long ModelInValid = 3;
        }
        public static class WebApiResultMessage
        {
            public const string UpdateSuccess = "WEB_API_RESULT_UPDATE_SUCCESS";
            public const string DeleteSuccess = "WEB_API_RESULT_DELETE_SUCCESS";
            public const string RestoreSuccess = "WEB_API_RESULT_RESTORE_SUCCESS";
            public const string InsertSuccess = "WEB_API_RESULT_INSERT_SUCCESS";
            public const string Success = "WEB_API_RESULT_SUCCESS";
            public const string Error = "WEB_API_RESULT_FAIL";
            public const string UpdateTokenNotMatch = "WEB_API_RESULT_UPDATETOKEN_NOT_MATCH";
        }
        public static class RecordStatusConfig
        {
            public const long Active = 0;
            public const long Deleted = 1;
        }
        public static class UserRecordStatus
        {
            public const long Active = 0;
            public const long Deleted = 1;
            public const long InActive = 2;
        }
        public static class AdminMenuRecordStatus
        {
            public const long Active = 0;
            public const long Deleted = 1;
            public const long Maintenance = 2;
        }
        public static class ClaimType
        {
            public const string Permission = "Permission";
        }
        public static class SystemConfigName
        {
            public const string PageDefaultNumber = "PageDefaultNumber";
        }
        public static class SessionName
        {
            public const string UserSession = "UserSession";
            public const string RoleSession = "RoleSession";
            public const string LanguageSession = "LanguageSession";
            public const string LanguageDetailSession = "LanguageDetailSession";
            public const string AdminMenuSession = "AdminMenuSession";
            public const string MasterListSession = "MasterListSession";
        }
    }
}
