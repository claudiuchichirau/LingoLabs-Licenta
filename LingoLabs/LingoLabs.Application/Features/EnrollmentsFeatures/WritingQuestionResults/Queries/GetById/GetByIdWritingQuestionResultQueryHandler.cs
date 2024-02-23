using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.WritingQuestionResults.Queries.GetById
{
    public class GetByIdWritingQuestionResultQueryHandler : IRequestHandler<GetByIdWritingQuestionResultQuery, WritingQuestionResultDto>
    {
        public GetByIdWritingQuestionResultQueryHandler(IWritingQuestionResultRepository writingQuestionResultRepository)
        {
            WritingQuestionResultRepository = writingQuestionResultRepository;
        }

        public IWritingQuestionResultRepository WritingQuestionResultRepository { get; }

        public async Task<WritingQuestionResultDto> Handle(GetByIdWritingQuestionResultQuery request, CancellationToken cancellationToken)
        {
            var writingQuestionResult = await WritingQuestionResultRepository.FindByIdAsync(request.Id);
            if (writingQuestionResult.IsSuccess)
            {
                return new GetSingleWritingQuestionResultDto
                {
                    QuestionResultId = writingQuestionResult.Value.QuestionResultId,
                    QuestionId = writingQuestionResult.Value.QuestionId,
                    LessonResultId = writingQuestionResult.Value.LessonResultId,
                    IsCorrect = writingQuestionResult.Value.IsCorrect,
                    ImageData = writingQuestionResult.Value.ImageData,
                    RecognizedText = writingQuestionResult.Value.RecognizedText
                };
            }

            return new GetSingleWritingQuestionResultDto();
        }
    }
}
