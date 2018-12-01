using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using WebCore.Services.Share.Languages;

namespace WebCore.Services.Share.ValidationHelper
{
    

    public class CustomRequiredAttribute : RequiredAttribute
        //, IClientModelValidator
    {
        public ILanguageProviderService languageProviderService;

        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    PlatformServices.Default.Application.
        //    this.languageProviderService = (ILanguageProviderService)CallContextServiceLocator.Locator.ServiceProvider
        //                    .GetService(typeof(ILanguageProviderService));

        //    throw new NotImplementedException();
        //}
    }
}
