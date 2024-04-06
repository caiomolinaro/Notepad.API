using Notepad.API.Features.Notes;

namespace Notepad.API.Shared.Data;

public interface INoteContext
{
    IMongoCollection<NoteEntity> Notes { get; }
}