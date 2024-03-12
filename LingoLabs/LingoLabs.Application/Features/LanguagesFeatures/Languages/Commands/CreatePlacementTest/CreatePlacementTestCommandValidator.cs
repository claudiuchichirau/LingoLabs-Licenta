using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreatePlacementTest
{
    public class CreatePlacementTestCommandValidator : AbstractValidator<CreatePlacementTestCommand>
    {
        private readonly IQuestionRepository questionRepository;
        private readonly ILanguageRepository languageRepository;

        public CreatePlacementTestCommandValidator(IQuestionRepository questionRepository, ILanguageRepository languageRepository)
        {
            this.questionRepository = questionRepository;
            this.languageRepository = languageRepository;

            RuleFor(p => p.LanguageId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.")
                .Must( (LanguageId) =>
                {
                    var languageExists = languageRepository.FindByIdAsync(LanguageId);

                    if(!languageExists.Result.IsSuccess)
                        return false;
                    return true;
                });

            RuleFor(p => p.Questions)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must((questions) =>
                {
                    if (questions.Count <= 2)
                        return false;

                    foreach (var question in questions)
                    {
                        if (question.QuestionId != Guid.Empty)
                        {
                            var questionExists = questionRepository.FindByIdAsync(question.QuestionId);

                            if (!questionExists.Result.IsSuccess)
                                return false;
                        }
                        else
                            return false;
                    }

                    return true;
                }).WithMessage("Questions must have at least 3 valid questions.");
        }
    }
}
