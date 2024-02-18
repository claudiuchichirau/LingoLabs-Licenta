using FluentValidation;
using LingoLabs.Domain.Entities;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Commands.CreateMatchingWordsQuestion
{
    public class CreateMatchingWordsQuestionCommandValidator: AbstractValidator<CreateMatchingWordsQuestionCommand>
    {
        public CreateMatchingWordsQuestionCommandValidator()
        {
            RuleFor(p => p.QuestionRequirement)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.QuestionLearningType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(type => type == LearningType.Auditory || type == LearningType.Visual || type == LearningType.Kinesthetic || type == LearningType.Logical)
                .WithMessage("{PropertyName} must have one of the following values: Auditory, Visual, Kinesthetic, Logical");

            RuleFor(p => p.WordPairs)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(words => words.Count >= 3)
                .WithMessage("{PropertyName} must have at least 3 words.");
        }
    }
}
