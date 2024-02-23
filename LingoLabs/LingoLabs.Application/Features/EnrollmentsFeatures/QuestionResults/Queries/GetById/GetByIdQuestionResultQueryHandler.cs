using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Queries.GetById
{
    public class GetByIdQuestionResultQueryHandler : IRequestHandler<GetByIdQuestionResultQuery, QuestionResultDto>
    {
        private readonly IQuestionResultRepository questionResultRepository;

        public GetByIdQuestionResultQueryHandler(IQuestionResultRepository questionResultRepository)
        {
            this.questionResultRepository = questionResultRepository;
        }
        public async Task<QuestionResultDto> Handle(GetByIdQuestionResultQuery request, CancellationToken cancellationToken)
        {
            var questionResult = await questionResultRepository.FindByIdAsync(request.Id);
            if(questionResult.IsSuccess)
            {
                return new QuestionResultDto
                {
                    QuestionResultId = questionResult.Value.QuestionResultId,
                    QuestionId = questionResult.Value.QuestionId,
                    LessonResultId = questionResult.Value.LessonResultId,
                    IsCorrect = questionResult.Value.IsCorrect,
                };
            }

            return new QuestionResultDto();
        }
    }
}
