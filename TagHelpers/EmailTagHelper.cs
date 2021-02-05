using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ePasieka.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string address { get; set; }
        public string annotation { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(annotation);
        }
    }
}
