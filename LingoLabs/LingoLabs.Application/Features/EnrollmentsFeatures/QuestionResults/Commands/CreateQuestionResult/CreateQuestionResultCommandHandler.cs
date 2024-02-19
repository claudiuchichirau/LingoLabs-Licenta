using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.CreateQuestionResult
{
    public class CreateQuestionResultCommandHandler : IRequestHandler<CreateQuestionResultCommand, CreateQuestionResultCommandResponse>
    {
        private readonly IQuestionResultRepository repository;

        public CreateQuestionResultCommandHandler(IQuestionResultRepository repository)
        {
            this.repository = repository;
        }
        public async Task<CreateQuestionResultCommandResponse> Handle(CreateQuestionResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuestionResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid) 
            { 
                return new CreateQuestionResultCommandResponse
                {
                    Success = false, 
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList() 
                };
            }

            var questionResult = QuestionResult.Create(request.QuestionId, request.LessonResultId, request.IsCorrect);
            if(questionResult.IsSuccess)
            {
                await repository.AddAsync(questionResult.Value);
                return new CreateQuestionResultCommandResponse
                {
                    QuestionResult = new CreateQuestionResultDto
                    {
                        QuestionResultId = questionResult.Value.QuestionResultId,
                        QuestionId = questionResult.Value.QuestionId,
                        LessonResultId = questionResult.Value.LessonResultId,
                        IsCorrect = questionResult.Value.IsCorrect
                    }
                };
            }

            return new CreateQuestionResultCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { questionResult.Error }
            };
        }
    }
}
