using Notepad.API.Features.Notepad;

namespace Notepad.API.DependencyInjection;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<INoteData, NoteData>();

        return services;
    }
}