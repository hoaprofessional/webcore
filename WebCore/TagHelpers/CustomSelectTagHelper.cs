using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Services.Share.Languages;

namespace WebCore.TagHelpers
{
    [HtmlTargetElement("select", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CustomSelectTagHelper : TagHelper
    {
        private readonly ILanguageProviderService languageProviderService;
        private const string ForAttributeName = "asp-for";
        private const string LanguageSupportAttributeName = "language-support";
        private const string ItemsAttributeName = "asp-items";

        /// <summary>
        /// Creates a new <see cref="InputTagHelper"/>.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/>.</param>
        public CustomSelectTagHelper( ILanguageProviderService languageProviderService)
        {
            this.languageProviderService = languageProviderService;
        }


        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }
        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<SelectListItem> Items { get; set; }

        [HtmlAttributeName(LanguageSupportAttributeName)]
        public bool LanguageSupport { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            TagHelperAttribute[] validations = output.Attributes.Where(x => x.Name.Contains("data-val-") && x.Name.Count(c => c == '-') == 2).ToArray();
            if (LanguageSupport && validations.Any())
            {
                foreach (TagHelperAttribute validation in validations)
                {
                    output.Attributes.RemoveAll(validation.Name);
                    string message = validation.Value.ToString();
                    output.Attributes.Add(validation.Name, languageProviderService.GetlangByKey(message));
                }
            }
            
            output.Attributes.Add("name", For);

            var innerHtml = await output.GetChildContentAsync();
            output.Content.AppendHtml(innerHtml);
        }
    }
}
