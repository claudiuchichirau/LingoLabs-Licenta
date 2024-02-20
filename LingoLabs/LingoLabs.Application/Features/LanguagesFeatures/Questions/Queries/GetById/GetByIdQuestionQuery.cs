using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetById
{
    public record class GetByIdQuestionQuery(Guid Id): IRequest<QuestionDto>;
}
