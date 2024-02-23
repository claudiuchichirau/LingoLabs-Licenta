using FluentValidation;
using LingoLabs.Application.Persistence;
using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LearningStyles.Commands.CreateLearningStyle
{
    public class CreateLearningStyleCommandValidator: AbstractValidator<CreateLearningStyleCommand>
    {
        private readonly ILearningStyleRepository repository;
        public CreateLearningStyleCommandValidator(ILearningStyleRepository repository)
        {
            this.repository = repository;

            RuleFor(p => p.LearningStyleName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLearningStyle(p.LearningStyleName, p.LearningType))
                .WithMessage("{PropertyName} must have one of the following values: Auditory, Visual, Kinesthetic, Logical and must be the same as LearningStyleName");
        }

        private async Task<bool> ValidateLearningStyle(string name, LearningType type)
        {
            if (await repository.ExistsLearningStyleAsync(type))
                return false;

            if (name == "Auditory" && type != LearningType.Auditory)
                return false;
            if (name == "Visual" && type != LearningType.Visual)
                return false;
            if (name == "Kinesthetic" && type != LearningType.Kinesthetic)
                return false;
            if (name == "Logical" && type != LearningType.Logical)
                return false;

            return true;
        }
    }
}
