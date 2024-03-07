using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.DeleteQuestionResult
{
    public class DeleteQuestionResultCommandHandler : IRequestHandler<DeleteQuestionResultCommand, DeleteQuestionResultCommandResponse>
    {
        private readonly IQuestionResultRepository questionResultRepository;

        public DeleteQuestionResultCommandHandler(IQuestionResultRepository questionResultRepository)
        {
            this.questionResultRepository = questionResultRepository;
        }
        public async Task<DeleteQuestionResultCommandResponse> Handle(DeleteQuestionResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteQuestionResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new DeleteQuestionResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
                
            }

            var questionResult = await questionResultRepository.FindByIdAsync(request.QuestionResultId);
            if (!questionResult.IsSuccess)
            {
                return new DeleteQuestionResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { questionResult.Error }
                };
            }

            await questionResultRepository.DeleteAsync(request.QuestionResultId);

            return new DeleteQuestionResultCommandResponse
            {
                Success = true
            };
        }
    }
}
