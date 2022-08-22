using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyASPProject.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        private const string EmailDomain = "rapidtech.id";
        public string MailTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var address = MailTo + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
        }
    }
}
