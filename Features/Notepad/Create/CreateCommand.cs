using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.Create;

public class CreateCommand : IRequest<Result<Guid>>
{
    public string Title { get; set; }
    public string Body { get; set; }
}