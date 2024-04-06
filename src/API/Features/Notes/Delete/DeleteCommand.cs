using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notes.Delete;

public sealed class DeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}