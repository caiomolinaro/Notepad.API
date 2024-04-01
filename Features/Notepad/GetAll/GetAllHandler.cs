using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.GetAll;

internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<NoteEntity>>>
{
    private readonly INoteData _noteData;

    public GetAllHandler(INoteData noteData)
    {
        _noteData = noteData;
    }

    public async Task<Result<IEnumerable<NoteEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<NoteEntity>>(await _noteData.GetAllNotesAsync(cancellationToken));
    }
}