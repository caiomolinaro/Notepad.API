using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.Update;

public class UpdateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}