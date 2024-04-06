using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notes.GetById;

internal sealed class GetByIdHandler(INoteData noteData, IValidator<GetByIdQuery> validator) : IRequestHandler<GetByIdQuery, Result<NoteEntity>>
{
    private readonly NoteData _noteData;
    private readonly IValidator<GetByIdQuery> _validator;

    public async Task<Result<NoteEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<NoteEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var noteEntity = await noteData.GetNoteByIdAsync(request.Id, cancellationToken);

        if (noteEntity is null)
        {
            return new Result<NoteEntity>(default, Errors.ReturnNoteNotFoundError());
        }

        return new Result<NoteEntity>(noteEntity);
    }
}