using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.Delete;

public sealed class DeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}