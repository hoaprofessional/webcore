using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCore.Utils.ModelHelper;

namespace WebCore.Api
{
    public class BaseApiController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;
        public BaseApiController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected IEnumerable<ModelStateErrorModel> GetModelErrors()
        {
            return ModelState
                .Where(x => x.Value.Errors.Any())
                .Select(x => new ModelStateErrorModel()
                {
                    PropertyName = x.Key,
                    ErrorMessages = x.Value.Errors.Select(e => e.ErrorMessage)
                });
        }
    }
}
