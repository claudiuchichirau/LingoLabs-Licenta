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

            RuleFor(p => p.QuestionsId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must((questionsId) =>
                {
                    if (questionsId.Count <= 2)
                        return false;

                    foreach (var questionId in questionsId)
                    {
                        if (questionId != Guid.Empty)
                        {
                            var questionExists = questionRepository.FindByIdAsync(questionId);

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
