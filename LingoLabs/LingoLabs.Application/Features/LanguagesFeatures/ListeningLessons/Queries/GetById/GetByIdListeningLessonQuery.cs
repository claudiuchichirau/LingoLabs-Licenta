using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries.GetById
{
    public record class GetByIdListeningLessonQuery(Guid Id): IRequest<GetSingleListeningLessonDto>;
}
