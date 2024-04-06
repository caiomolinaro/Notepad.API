namespace Notepad.API.Features.Notes.Create;

public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.Title)
            .NotEmpty()
            .WithMessage("Title Cannot be Empty");

        RuleFor(expression => expression.Body)
            .NotEmpty()
            .WithMessage("Body Cannot be Empty");
    }
}