using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notes.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<NoteEntity>>;