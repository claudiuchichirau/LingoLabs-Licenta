using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Queries.GetById
{
    public record class GetByIdMatchingQuestionQuery(Guid Id) : IRequest<MatchingWordsQuestionDto>;
}
