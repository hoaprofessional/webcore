using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebCore.Services.Share.Languages;
using WebCore.Services.Share.RecordStatuss;
using WebCore.Utils.Commons;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.RecordStatuss
{
    public class RecordStatusHelper : IRecordStatusHelper
    {
        private readonly ILanguageProviderService languageProviderService;
        public RecordStatusHelper(ILanguageProviderService languageProviderService) 
        {
            this.languageProviderService = languageProviderService;
        }
        public SelectList GetRecordStatusCombobox()
        {
            List<ComboboxResult<long, string>> recordStatusComboboxs = new List<ComboboxResult<long, string>>()
            {
                new ComboboxResult<long, string>()
                {
                    Value = ConstantConfig.RecordStatusConfig.Active,
                    Display = languageProviderService.GetlangByKey("LBL_RECORDSTATUS_ACTIVE")
                },
                new ComboboxResult<long, string>()
                {
                    Value = ConstantConfig.RecordStatusConfig.Deleted,
                    Display = languageProviderService.GetlangByKey("LBL_RECORDSTATUS_DELETE")
                }
            };
            return recordStatusComboboxs.ToSelectList();
        }
    }
}
