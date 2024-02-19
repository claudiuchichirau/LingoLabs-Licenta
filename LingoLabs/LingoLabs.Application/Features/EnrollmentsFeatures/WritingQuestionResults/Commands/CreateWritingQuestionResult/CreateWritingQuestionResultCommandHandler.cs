using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.WritingQuestionResults.Commands.CreateWritingQuestionResult
{
    public class CreateWritingQuestionResultCommandHandler: IRequestHandler<CreateWritingQuestioResultCommand, CreateWritingQuestionResultCommandResponse>
    {
        private readonly IWritingQuestionResultRepository repository;

        public CreateWritingQuestionResultCommandHandler(IWritingQuestionResultRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateWritingQuestionResultCommandResponse> Handle(CreateWritingQuestioResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateWritingQuestionResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if(!validationResult.IsValid)
            {
                return new CreateWritingQuestionResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            if (request.ImageData != default && request.RecognizedText == default)
            {
                var writingQuestionResult = WritingQuestionResult.CreateFromImage(request.QuestionId, request.LessonResultId, request.IsCorrect, request.ImageData);

                if(writingQuestionResult.IsSuccess)
                {
                    await repository.AddAsync(writingQuestionResult.Value);
                    return new CreateWritingQuestionResultCommandResponse
                    {
                        WritingQuestionResult = new CreateWritingQuestionResultDto
                        {
                            QuestionResultId = writingQuestionResult.Value.QuestionResultId,
                            QuestionId = writingQuestionResult.Value.QuestionId,
                            LessonResultId = writingQuestionResult.Value.LessonResultId,
                            IsCorrect = writingQuestionResult.Value.IsCorrect,
                            ImageData = writingQuestionResult.Value.ImageData
                        }
                    };
                }
            }
            else if (request.ImageData == default && request.RecognizedText != default)
            {
                var writingQuestionResult = WritingQuestionResult.CreateFromText(request.QuestionId, request.LessonResultId, request.IsCorrect, request.RecognizedText);

                if(writingQuestionResult.IsSuccess)
                {
                    await repository.AddAsync(writingQuestionResult.Value);
                    return new CreateWritingQuestionResultCommandResponse
                    {
                        WritingQuestionResult = new CreateWritingQuestionResultDto
                        {
                            QuestionResultId = writingQuestionResult.Value.QuestionResultId,
                            QuestionId = writingQuestionResult.Value.QuestionId,
                            LessonResultId = writingQuestionResult.Value.LessonResultId,
                            IsCorrect = writingQuestionResult.Value.IsCorrect,
                            RecognizedText = writingQuestionResult.Value.RecognizedText
                        }
                    };
                }
            }

            return new CreateWritingQuestionResultCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { "Error creating WritingQuestionResult" }
            };
        }
    }
}
