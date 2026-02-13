using FluentValidation;
using ToDoListApp.Application.Models;

namespace ToDoListApp.Application.Validators;
public class ToDoListUpdateModelValidator : AbstractValidator<ToDoListUpdateModel>
{
    private const int MIN_NAME_LENGTH = 1;
    private const int MAX_NAME_LENGTH = 255;

    public ToDoListUpdateModelValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ToDoList Id cannot be empty.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User Id cannot be empty.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");

        RuleFor(x => x.Name.Length)
            .LessThan(MIN_NAME_LENGTH)
            .GreaterThan(MAX_NAME_LENGTH)
            .WithMessage($"Length of User Name must be greater than {MIN_NAME_LENGTH} and less than {MAX_NAME_LENGTH}.");
    }
}
