
namespace Notepad.API.Features.Notepad;

internal interface INoteData
{
    Task CreateNoteAsync(NoteEntity note, CancellationToken cancellationToken);

    Task<IEnumerable<NoteEntity>> GetAllNotesAsync(CancellationToken cancellationToken);
    Task<NoteEntity> GetNoteByIdAsync(Guid id, CancellationToken cancellationToken);
}