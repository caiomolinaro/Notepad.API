using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.GetById;

internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<NoteEntity>>
{
    private readonly NoteData _noteData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(NoteData noteData, IValidator<GetByIdQuery> validator)
    {
        _noteData = noteData;
        _validator = validator;
    }

    public async Task<Result<NoteEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<NoteEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var noteEntity = await _noteData.GetNoteByIdAsync(request.Id, cancellationToken);

        if (noteEntity is null)
        {
            return new Result<NoteEntity>(default, Errors.ReturnNoteNotFoundError());
        }

        return new Result<NoteEntity>(noteEntity);
    }
}