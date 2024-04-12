using LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetById
{
    public class GetByIdQuestionQueryHandler : IRequestHandler<GetByIdQuestionQuery, QuestionDto>
    {
        private readonly IQuestionRepository repository;

        public GetByIdQuestionQueryHandler(IQuestionRepository repository)
        {
            this.repository = repository;
        }
        public async Task<QuestionDto> Handle(GetByIdQuestionQuery request, CancellationToken cancellationToken)
        {
            var question = await repository.FindByIdAsync(request.Id);
            if(question.IsSuccess)
            {
                return new GetSingleQuestionDto
                {
                    QuestionId = question.Value.QuestionId,
                    QuestionRequirement = question.Value.QuestionRequirement,
                    QuestionType = question.Value.QuestionType,
                    QuestionPriorityNumber = question.Value.QuestionPriorityNumber,
                    QuestionChoices = question.Value.QuestionChoices.Select(c => new Choices.Queries.ChoiceDto
                    {
                        ChoiceId = c.ChoiceId,
                        ChoiceContent = c.ChoiceContent,
                        IsCorrect = c.IsCorrect,
                        QuestionId = c.QuestionId
                    }).ToList(),

                    QuestionImageData = question.Value.QuestionImageData,
                    QuestionVideoLink = question.Value.QuestionVideoLink,
                    LessonId = question.Value.LessonId,
                    LanguageId = question.Value.LanguageId
                };
            }

            return new QuestionDto();
        }
    }
}
