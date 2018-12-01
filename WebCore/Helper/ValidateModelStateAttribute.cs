using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WebCore.Utils.ModelHelper;

namespace WebCore.Helper
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState
                .Where(x => x.Value.Errors.Any())
                .Select(x => new ModelStateErrorModel()
                {
                    PropertyName = x.Key,
                    ErrorMessages = x.Value.Errors.Select(e => e.ErrorMessage)
                }));
            }
        }
    }
}
