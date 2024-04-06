using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notes.Update;

internal sealed class UpdateHandler(INoteData noteData, IValidator<UpdateCommand> validator) : IRequestHandler<UpdateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new NoteEntity
        {
            Id = request.Id,
            CreationDate = DateTime.UtcNow,
            Title = request.Title,
            Body = request.Body
        };

        await noteData.UpdateNoteAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}