using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad.Delete;

internal sealed class DeleteHandler(INoteData noteData, IValidator<DeleteCommand> validator) : IRequestHandler<DeleteCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var noteEntity = await noteData.GetNoteByIdAsync(request.Id, cancellationToken);

        if (noteEntity is null)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnNoteNotFoundError());
        }

        await noteData.DeleteNoteAsync(request.Id, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}