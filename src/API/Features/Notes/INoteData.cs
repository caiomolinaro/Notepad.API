namespace Notepad.API.Features.Notes;

internal interface INoteData
{
    Task CreateNoteAsync(NoteEntity note, CancellationToken cancellationToken);

    Task<bool> DeleteNoteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<NoteEntity>> GetAllNotesAsync(CancellationToken cancellationToken);

    Task<NoteEntity> GetNoteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> UpdateNoteAsync(NoteEntity note, CancellationToken cancellationToken);
}