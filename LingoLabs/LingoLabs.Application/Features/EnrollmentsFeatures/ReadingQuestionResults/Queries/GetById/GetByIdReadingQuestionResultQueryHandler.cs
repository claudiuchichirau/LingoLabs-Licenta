using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Queries.GetById
{
    public class GetByIdReadingQuestionResultQueryHandler : IRequestHandler<GetByIdReadingQuestionResultQuery, ReadingQuestionResultDto>
    {
        private readonly IReadingQuestionResultRepository readingQuestionResultRepository;

        public GetByIdReadingQuestionResultQueryHandler(IReadingQuestionResultRepository readingQuestionResultRepository)
        {
            this.readingQuestionResultRepository = readingQuestionResultRepository;
        }
        public async Task<ReadingQuestionResultDto> Handle(GetByIdReadingQuestionResultQuery request, CancellationToken cancellationToken)
        {
            var readingQuestionResult = await readingQuestionResultRepository.FindByIdAsync(request.Id);
            if(readingQuestionResult.IsSuccess)
            {
                return new GetSingleReadingQuestionResultDto
                {
                    QuestionResultId = readingQuestionResult.Value.QuestionResultId,
                    QuestionId = readingQuestionResult.Value.QuestionId,
                    LessonResultId = readingQuestionResult.Value.LessonResultId,
                    IsCorrect = readingQuestionResult.Value.IsCorrect,
                    AudioData = readingQuestionResult.Value.AudioData,
                    RecognizedText = readingQuestionResult.Value.RecognizedText
                };
            }

            return new GetSingleReadingQuestionResultDto();
        }
    }
}
