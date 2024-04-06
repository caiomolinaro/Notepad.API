namespace Notepad.API.Features.Notes.GetById;

public sealed class GetByIdValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdValidator()
    {
        RuleFor(expression => expression.Id)
            .NotEmpty()
            .WithMessage("Note Id cannot be empty");
    }
}