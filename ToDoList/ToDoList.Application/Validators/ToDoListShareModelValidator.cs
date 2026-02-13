using FluentValidation;
using ToDoListApp.Application.Models;

namespace ToDoListApp.Application.Validators;
public class ToDoListShareModelValidator : AbstractValidator<ToDoListShareModel>
{
    public ToDoListShareModelValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User Id cannot be empty.");

        RuleFor(x => x.ReceiverUserId)
            .NotEmpty()
            .WithMessage("ReceiverUser Id cannot be empty.");

        RuleFor(x => x.ToDoListId)
            .NotEmpty()
            .WithMessage("ToDoList Id cannot be empty.");
    }
}
