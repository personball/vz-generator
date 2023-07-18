namespace vz_generator.Generator.Settings.SettingResolvers
{
    public interface IGeneratorSettingResolver
    {
        Task ResolveAsync(ResolveContext context);
    }
}