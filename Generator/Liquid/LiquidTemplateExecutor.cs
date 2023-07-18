using System.CommandLine.Invocation;
using System.Text.Json;
using Scriban;
using Scriban.Runtime;
using vz_generator.Commands.Settings;
using vz_generator.Generator.Liquid.Scriban;

namespace vz_generator.Generator.Liquid;
public class LiquidTemplateExecutor
{
    private readonly GeneratorSetting _setting;
    private readonly InvocationContext _context;
    public LiquidTemplateExecutor(GeneratorSetting setting, InvocationContext context)
    {
        _setting = setting;
        _context = context;
    }

    public async Task ExecuteAsync()
    {
        // load naming functions
        var vzFuncs = new VzStringUtils();
        var context = new TemplateContext();
        context.PushGlobal(vzFuncs);

        if (_setting.Variables.Any())
        {
            var variableObj = new ScriptObject();
            foreach (var item in _setting.Variables)
            {
                if (item.Type == TemplateVariableType.String)
                {
                    // load string variables
                    variableObj.Add(item.Name, item.DefaultValue);
                }

                if (item.Type == TemplateVariableType.JsonFile)
                {
                    // TODO: load json object variables
                    variableObj.Add("json", JsonDocument.Parse("""{"name":"wangbocheng","value":{"x":1}}"""));
                }
            }


            context.PushGlobal(variableObj);
        }

        // TODO: enumerate templates and paths




        var template = Template.Parse("""
        author: {{json.name|string.upcase}}
        NameIt == {{'nameIt'|pascal_case}}
        nameIt == {{'NameIt'|camel_case}}
        name-it == {{'NameIt'|kebab_case}}
        name_it == {{'NameIt'|snake_case}}
        people == {{'person'|pluralize}}
        person == {{'people'|singularize}}
        """);
        var result = template.Render(context);

        Console.WriteLine(result);

        // throw new NotImplementedException();
    }


}