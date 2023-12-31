using Panic.StringUtils;
using Scriban.Runtime;

namespace vz_generator.Generator.Liquid.Scriban;

public class VzStringUtils : ScriptObject
{
    /// <summary>
    /// ```
    /// {{'nameIt'|pascal_case}} 
    /// ```
    /// ```
    /// NameIt
    /// ```
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string PascalCase(string text)
    {
        return StringUtils.ToPascalCase(text);
    }

    /// <summary>
    /// ```
    /// {{'NameIt'|camel_case}}
    /// ```
    /// ```
    /// nameIt
    /// ```
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string CamelCase(string text)
    {
        return StringUtils.ToCamelCase(text);
    }

    /// <summary>
    /// ```
    /// {{'NameIt'|kebab_case}}
    /// ```
    /// ```
    /// name-it
    /// ```
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string KebabCase(string text)
    {
        return StringUtils.ToKebabCase(text);
    }

    /// <summary>
    /// ```
    /// {{'NameIt'|snake_case}}
    /// ```
    /// ```
    /// name_it
    /// ```
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string SnakeCase(string text)
    {
        return StringUtils.ToSnakeCase(text);
    }

    /// <summary>
    /// ```
    /// {{'person'|pluralize}}
    /// ```
    /// ```
    /// people
    /// ```
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Pluralize(string text)
    {
        return PluralizerUtils.Pluralizer.Pluralize(text);
    }

    /// <summary>
    /// ```
    /// {{'people'|singularize}}
    /// ```
    /// ```
    /// person
    /// ```
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Singularize(string text)
    {
        return PluralizerUtils.Pluralizer.Singularize(text);
    }
}