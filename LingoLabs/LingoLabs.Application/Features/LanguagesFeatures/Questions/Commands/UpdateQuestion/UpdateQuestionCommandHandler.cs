using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, UpdateQuestionCommandResponse>
    {
        private readonly IQuestionRepository questionRepository;
        private readonly ILanguageRepository languageRepository;

        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository, ILanguageRepository languageRepository)
        {
            this.questionRepository = questionRepository;
            this.languageRepository = languageRepository;
        }
        public async Task<UpdateQuestionCommandResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuestionCommandValidator(languageRepository, questionRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var question = await questionRepository.FindByIdAsync(request.QuestionId);

            if (!question.IsSuccess)
            {
                return new UpdateQuestionCommandResponse 
                {
                    Success = false,
                    ValidationsErrors = new List<string> { question.Error }
                };
            }

            var updateQuestioDto = request.UpdateQuestionDto;

            question.Value.UpdateQuestion(
                updateQuestioDto.QuestionRequirement,
                updateQuestioDto.QuestionLearningType,
                updateQuestioDto.QuestionImageData,
                updateQuestioDto.QuestionVideoLink,
                updateQuestioDto.LanguageId,
                updateQuestioDto.QuestionPriorityNumber);

            await questionRepository.UpdateAsync(question.Value);

            return new UpdateQuestionCommandResponse
            {
                Success = true,
                UpdateQuestion = new UpdateQuestionDto
                {
                    QuestionRequirement = question.Value.QuestionRequirement,
                    QuestionLearningType = question.Value.QuestionLearningType,
                    QuestionImageData = question.Value.QuestionImageData,
                    QuestionVideoLink = question.Value.QuestionVideoLink,
                    QuestionPriorityNumber = question.Value.QuestionPriorityNumber,
                }
            };
        }
    }
}
