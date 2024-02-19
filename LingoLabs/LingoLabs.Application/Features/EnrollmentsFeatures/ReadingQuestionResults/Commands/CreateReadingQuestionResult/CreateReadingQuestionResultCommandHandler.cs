using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Commands.CreateReadingQuestionResult
{
    public class CreateReadingQuestionResultCommandHandler: IRequestHandler<CreateReadingQuestionResultCommand, CreateReadingQuestionResultCommandResponse>
    {
        private readonly IReadingQuestionResultRepository repository;

        public CreateReadingQuestionResultCommandHandler(IReadingQuestionResultRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateReadingQuestionResultCommandResponse> Handle(CreateReadingQuestionResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateReadingQuestionResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateReadingQuestionResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            
            var readingQuestionResult = ReadingQuestionResult.Create(request.QuestionId, request.LessonResultId, request.IsCorrect, request.AudioData);
            if (readingQuestionResult.IsSuccess)
            {
                await repository.AddAsync(readingQuestionResult.Value);
                return new CreateReadingQuestionResultCommandResponse
                {
                    ReadingQuestionResult = new CreateReadingQuestionResultDto
                    {
                        QuestionResultId = readingQuestionResult.Value.QuestionResultId,
                        QuestionId = readingQuestionResult.Value.QuestionId,
                        LessonResultId = readingQuestionResult.Value.LessonResultId,
                        IsCorrect = readingQuestionResult.Value.IsCorrect,
                        AudioData = readingQuestionResult.Value.AudioData
                    }
                };
            }

            return new CreateReadingQuestionResultCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { readingQuestionResult.Error }
            };

        }
    }
}
