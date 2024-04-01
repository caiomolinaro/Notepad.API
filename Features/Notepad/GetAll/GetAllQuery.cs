using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<NoteEntity>>>;