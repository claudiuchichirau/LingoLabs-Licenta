using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdatePlacementTest
{
    public class UpdatePlacementTestCommandValidator: AbstractValidator<UpdatePlacementTestCommand>
    {
        private readonly IQuestionRepository questionRepository;
        public UpdatePlacementTestCommandValidator(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;

            RuleFor(x => x.QuestionsId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must((questionsIds) =>
                {
                    if (questionsIds.Count <= 2)
                        return false;

                    foreach (var questionId in questionsIds)
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
