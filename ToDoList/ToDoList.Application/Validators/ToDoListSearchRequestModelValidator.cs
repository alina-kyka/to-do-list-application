using FluentValidation;
using ToDoListApp.Application.Models;

namespace ToDoListApp.Application.Validators;
public class ToDoListSearchRequestModelValidator : AbstractValidator<ToDoListSearchRequestModel>
{
    public ToDoListSearchRequestModelValidator()
    {
        RuleFor(x => x.UserId)
           .NotEmpty()
           .WithMessage("User Id cannot be empty.");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number cannot be zero or negative");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page size cannot be zero or negative");
    }
}
