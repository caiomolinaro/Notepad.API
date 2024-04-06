using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notes.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<NoteEntity>>>;