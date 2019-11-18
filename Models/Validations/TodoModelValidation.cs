namespace TodoApi.Models.Validations
{
    using FluentValidation;
    using FluentValidation.Validators;
    using System.Threading;
    using System.Threading.Tasks;
    using TodoApi.Models.TodoModel;
    using TodoApi.Services.Todo;

    public class TodoModelValidation : AbstractValidator<TodoCreateModel>
    {
        private readonly ITodoService _todoService;

        public TodoModelValidation(
          ITodoService todoService
        ) {
            _todoService = todoService;

            RuleFor(e => e.Description)
                .MaximumLength(255).WithMessage("the max number of characters is 255");

            RuleFor(e => e.Title)
                .CustomAsync(IsUniqueTitle);
        }

        private async Task IsUniqueTitle(string propToValidate, CustomContext context, CancellationToken cancellationToken)
        {
            var isUnique = await _todoService.IsUniqueTitle(propToValidate);

            if (isUnique) context.AddFailure("the title is ready exists");
        }
    }

}
