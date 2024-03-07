using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.UpdateQuestionResult
{
    public class UpdateQuestionResultCommandHandler : IRequestHandler<UpdateQuestionResultCommand, UpdateQuestionResultCommandResponse>
    {
        private readonly IQuestionResultRepository questionResultRepository;

        public UpdateQuestionResultCommandHandler(IQuestionResultRepository questionResultRepository)
        {
            this.questionResultRepository = questionResultRepository;
        }
        public async Task<UpdateQuestionResultCommandResponse> Handle(UpdateQuestionResultCommand request, CancellationToken cancellationToken)
        {
            var questionResult = await questionResultRepository.FindByIdAsync(request.QuestionResultId);

            if(!questionResult.IsSuccess) 
            {
                return new UpdateQuestionResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { questionResult.Error }
                };
            }

            var updateQuestionResultDto = request.UpdateQuestionResultDto;

            questionResult.Value.UpdateQuestionResult(updateQuestionResultDto.IsCorrect);

            await questionResultRepository.UpdateAsync(questionResult.Value);

            return new UpdateQuestionResultCommandResponse
            {
                Success = true,
                UpdateQuestionResult = new UpdateQuestionResultDto
                {
                    IsCorrect = questionResult.Value.IsCorrect
                }
            };
        }
    }
}
