using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.Create;

internal sealed class CreateHandler(INoteData noteData, IValidator<CreateCommand> validator) : IRequestHandler<CreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new NoteEntity
        {
            Id = Guid.NewGuid(),
            Title = request.Title!,
            CreationDate = DateTime.UtcNow,
            Body = request.Body!
        };

        await noteData.CreateNoteAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}