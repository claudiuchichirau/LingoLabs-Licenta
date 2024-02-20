using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Commands.CreateWordPair
{
    public class CreateWordPairCommandValidator: AbstractValidator<CreateWordPairCommand>
    {
        public CreateWordPairCommandValidator()
        {
            RuleFor(p => p.KeyWord)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");
            
            RuleFor(p => p.ValueWord)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(p => p.MatchingWordsQuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");
        }
    }
}
