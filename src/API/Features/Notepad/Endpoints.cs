using Notepad.API.Features.Notepad.Create;
using Notepad.API.Features.Notepad.Delete;
using Notepad.API.Features.Notepad.GetAll;
using Notepad.API.Features.Notepad.GetById;
using Notepad.API.Features.Notepad.Update;
using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad;

public sealed class Endpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/notes")
            .WithTags("Notes");

        group.MapGet(string.Empty, GetNotesAsync);
        group.MapGet("/{id:guid}", GetNoteByIdAsync);
        group.MapPost(string.Empty, CreateNoteAsync);
        group.MapPut(string.Empty, UpdateNoteAsync);
        group.MapDelete("/{id:guid}", DeleteNoteAsync);
    }

    public static async Task<IResult> GetNotesAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Notes retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<NoteEntity>>(result.Data));
    }

    public static async Task<IResult> GetNoteByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Note by id retrieved with success: {input}", query);

        return Results.Ok(new Response<NoteEntity>(result.Data));
    }

    public static async Task<IResult> CreateNoteAsync(CreateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Title = request.Title,
            Body = request.Body,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Note created with success: {input}", command);

        return Results.Created("$/notes/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateNoteAsync(UpdateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Title = request.Title!,
            Body = request.Body!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Note updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteNoteAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Note deleted with success: {input}", command);

        return Results.NoContent();
    }
}