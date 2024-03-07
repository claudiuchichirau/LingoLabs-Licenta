using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, UpdateQuestionCommandResponse>
    {
        private readonly IQuestionRepository questionRepository;

        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
        public async Task<UpdateQuestionCommandResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
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

            question.Value.UpdateQuestion(updateQuestioDto.QuestionRequirement,
                updateQuestioDto.QuestionLearningType,
                updateQuestioDto.QuestionImageData,
                updateQuestioDto.QuestionVideoLink);

            await questionRepository.UpdateAsync(question.Value);

            return new UpdateQuestionCommandResponse
            {
                Success = true,
                UpdateQuestion = new UpdateQuestionDto
                {
                    QuestionRequirement = question.Value.QuestionRequirement,
                    QuestionLearningType = question.Value.QuestionLearningType,
                    QuestionImageData = question.Value.QuestionImageData,
                    QuestionVideoLink = question.Value.QuestionVideoLink
                }
            };
        }
    }
}
