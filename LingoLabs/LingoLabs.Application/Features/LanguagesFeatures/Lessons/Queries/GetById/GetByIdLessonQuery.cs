using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetById
{
    public record class GetByIdLessonQuery(Guid Id): IRequest<GetSingleLessonDto>;
}
