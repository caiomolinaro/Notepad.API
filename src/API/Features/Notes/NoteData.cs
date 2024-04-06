using Notepad.API.Shared.Data;

namespace Notepad.API.Features.Notes;

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

    public async Task<bool> DeleteNoteAsync(Guid id, CancellationToken cancellationToken)
    {
        FilterDefinition<NoteEntity> filter = Builders<NoteEntity>.Filter.Eq(note => note.Id, id);
        DeleteResult deleteResult = await _context.Notes.DeleteOneAsync(filter, cancellationToken);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<bool> UpdateNoteAsync(NoteEntity note, CancellationToken cancellationToken)
    {
        FilterDefinition<NoteEntity> filter = Builders<NoteEntity>.Filter.Eq(n => n.Id, note.Id);
        var updateOptions = new ReplaceOptions();
        var updateResult = await _context.Notes.ReplaceOneAsync(filter, note, updateOptions, cancellationToken);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
}