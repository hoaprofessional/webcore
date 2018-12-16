using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Languages;
using WebCore.Services.Share.MasterLists;
using WebCore.Utils.Commons;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.MasterLists
{
    public class MasterListProvider : IMasterListProvider
    {
        private readonly IRepository<MasterList, int> masterListRepository;
        private readonly ILanguageProviderService languageProvider;

        public MasterListProvider(IRepository<MasterList, int> masterListRepository, ILanguageProviderService languageProvider)
        {
            this.masterListRepository = masterListRepository;
            this.languageProvider = languageProvider;
        }

        public SelectList SelectItemByGroup(string group)
        {
            System.Collections.Generic.List<ComboboxResult<string, string>> result = masterListRepository
                            .GetByCondition(x => x.Group == group)
                            .Select(x => new ComboboxResult<string, string>()
                            {
                                Value = x.Value,
                                Display = x.Value
                            }).ToList();

            result.ForEach(x =>
            {
                x.Display = languageProvider.GetlangByKey($"LBL_MASTERLISTVALUE_{x.Value}");
            });
            return result.ToSelectList();
        }
    }
}
