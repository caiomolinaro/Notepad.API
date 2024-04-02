using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.GetAll;

internal sealed class GetAllHandler(INoteData noteData) : IRequestHandler<GetAllQuery, Result<IEnumerable<NoteEntity>>>
{
    private readonly INoteData _noteData;

    public async Task<Result<IEnumerable<NoteEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<NoteEntity>>(await noteData.GetAllNotesAsync(cancellationToken));
    }
}