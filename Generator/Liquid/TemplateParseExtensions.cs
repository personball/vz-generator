using Scriban;

namespace vz_generator.Generator.Liquid
{
    public static class TemplateParseExtensions
    {
        public static string RenderContent(this string tplContent, TemplateContext tplContext)
        {
            var template = Template.Parse(tplContent);
            return template.Render(tplContext);
        }
    }
}