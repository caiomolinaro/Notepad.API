using Notepad.API.Features.Notepad;
using Notepad.API.Features.Notepad.Create;
using Notepad.API.Features.Notepad.GetById;
using Notepad.API.Shared.Data;

namespace Notepad.API.DependencyInjection;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<INoteData, NoteData>();

        services.AddSingleton<INoteContext, NoteContext>();
        services.AddTransient<IValidator<CreateCommand>, CreateValidator>();
        services.AddTransient<NoteData>();
        services.AddTransient<IValidator<GetByIdQuery>, GetByIdValidator>();

        return services;
    }
}