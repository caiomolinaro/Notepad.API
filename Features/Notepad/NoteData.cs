using Notepad.API.Shared.Data;

namespace Notepad.API.Features.Notepad;

internal sealed class NoteData : INoteData
{
    private readonly INoteContext _context;

    public NoteData(INoteContext context)
    {
        _context = context;
    }

    public async Task CreateNoteAsync(NoteEntity note, CancellationToken cancellationToken)
    {
        await _context.Notes.InsertOneAsync(note, cancellationToken);
    }

    public async Task<IEnumerable<NoteEntity>> GetAllNotesAsync(CancellationToken cancellationToken)
    {
        return await _context.Notes.Find(notes => true).ToListAsync(cancellationToken);
    }

    public async Task<NoteEntity> GetNoteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Notes.Find(note => note.Id == id).FirstOrDefaultAsync(cancellationToken);
    }
}