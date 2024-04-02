namespace Notepad.API.Features.Notepad.Update;

public sealed class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Id)
          .NotEmpty()
          .WithMessage("Id Cannot be Empty");

        RuleFor(expression => expression.Title)
           .NotEmpty()
           .WithMessage("Title Cannot be Empty");

        RuleFor(expression => expression.Body)
            .NotEmpty()
            .WithMessage("Body Cannot be Empty");
    }
}