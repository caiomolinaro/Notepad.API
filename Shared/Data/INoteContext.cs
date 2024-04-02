using Notepad.API.Features.Notepad;

namespace Notepad.API.Shared.Data;

public interface INoteContext
{
    IMongoCollection<NoteEntity> Notes { get; }
}