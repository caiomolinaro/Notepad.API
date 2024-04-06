namespace Notepad.API.Features.Notepad.Update;

public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}