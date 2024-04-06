namespace Notepad.API.Features.Notes.Create;

public sealed class CreateRequest
{
    public string? Title { get; set; }
    public string? Body { get; set; }
}