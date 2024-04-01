using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<NoteEntity>>;