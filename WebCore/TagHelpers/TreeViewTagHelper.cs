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
        [HtmlAttributeName("treeview-model")]
        public ITreeViewModel Model { get; set; }

        [HtmlAttributeName("treeview-partialview-child")]
        public string ViewChild { get; set; }

        [HtmlAttributeName("treeview-isrenderrootnode")]
        public bool IsRenderRootNode { get; set; }

        [HtmlAttributeName("treeview-ulclass")]
        public string UlClass { get; set; }

        [HtmlAttributeName("treeview-rootulclass")]
        public string RootUlClass { get; set; }

        [HtmlAttributeName("treeview-liclass")]
        public string LiClass { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlHelper htmlHelper;

        public TreeViewTagHelper(IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            UlClass = "";
            LiClass = "";
        }

        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Model == null)
            {
                return;
            }
            (htmlHelper as IViewContextAware).Contextualize(ViewContext);
            TagBuilder rootNode = new TagBuilder("ul");
            rootNode.AddCssClass(RootUlClass);
            if (IsRenderRootNode)
            {
                TagBuilder tagContent = await CreateTreeViewTagAsync(Model);
                rootNode.InnerHtml.AppendHtml(tagContent);
            }
            else
            {
                foreach(var child in Model.Childs)
                {
                    TagBuilder tagContent = await CreateTreeViewTagAsync(child);
                    rootNode.InnerHtml.AppendHtml(tagContent);
                }
            }
            output.Content.AppendHtml(rootNode);
        }

        private async Task<TagBuilder> CreateTreeViewTagAsync(ITreeViewModel model)
        {
            TagBuilder tagBuilder = new TagBuilder("li");
            tagBuilder.AddCssClass("treeview-item");
            tagBuilder.AddCssClass(LiClass);

            IHtmlContent innerContentWithModel = await htmlHelper.PartialAsync(ViewChild, model);

            tagBuilder.InnerHtml.AppendHtml(innerContentWithModel);

            if (model.Childs.Count > 0)
            {
                tagBuilder.AddCssClass("has-children");

                TagBuilder ul = new TagBuilder("ul");
                ul.AddCssClass(UlClass);
                ul.Attributes.Add("id", "treeview-childs-" + model.Key);
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
