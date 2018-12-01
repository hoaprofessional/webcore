using Microsoft.AspNetCore.Mvc;
using System;
using WebCore.Services.Share.Languages;
using WebCore.Services.Share.Languages.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : BaseApiController
    {
        private ILanguageProviderService languageService;

        public LanguageController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.languageService = (ILanguageProviderService)serviceProvider.GetService(typeof(ILanguageProviderService)) ;
        }

        [HttpGet("{code}")]
        public ListResult<LanguageDetailDto> GetLanguageByCode(string code)
        {
            return languageService.GetLanguagesByCode(code);
        }

        [HttpPost]
        public StatusCodeResult UpdateLanguage(LanguageInsertUpdateInput input)
        {
            //languageService.UpdateLanguage(input);
            return Ok();
        }
    }
}
