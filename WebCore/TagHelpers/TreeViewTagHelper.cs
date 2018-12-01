using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using WebCore.Utils.TreeViewHelper;

namespace WebCore.TagHelpers
{
    [HtmlTargetElement("treeview")]
    public class TreeViewTagHelper : TagHelper
    {
        [HtmlAttributeName("model")]
        public ITreeViewModel Model { get; set; }

        [HtmlAttributeName("partialview-child")]
        public string ViewChild { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlHelper htmlHelper;

        public TreeViewTagHelper(IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }


        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            (htmlHelper as IViewContextAware).Contextualize(ViewContext);
            TagBuilder rootNode = new TagBuilder("ul");
            TagBuilder tagContent = await CreateTreeViewTagAsync(Model);
            rootNode.InnerHtml.AppendHtml(tagContent);
            output.Content.AppendHtml(rootNode);
        }

        private async Task<TagBuilder> CreateTreeViewTagAsync(ITreeViewModel model)
        {
            TagBuilder tagBuilder = new TagBuilder("li");
            tagBuilder.AddCssClass("treeview-item");

            IHtmlContent innerContentWithModel = await htmlHelper.PartialAsync(ViewChild, model);

            tagBuilder.InnerHtml.AppendHtml(innerContentWithModel);

            if (model.Childs.Count > 0)
            {
                tagBuilder.AddCssClass("has-children");

                TagBuilder ul = new TagBuilder("ul");
                foreach (ITreeViewModel child in model.Childs)
                {
                    TagBuilder childLi = await CreateTreeViewTagAsync(child);
                    ul.InnerHtml.AppendHtml(childLi);
                }
                tagBuilder.InnerHtml.AppendHtml(ul);
            }
            return tagBuilder;
        }

    }
}
