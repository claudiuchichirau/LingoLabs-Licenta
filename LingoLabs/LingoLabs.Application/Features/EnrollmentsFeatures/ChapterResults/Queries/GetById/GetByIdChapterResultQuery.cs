using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Queries.GetById
{
    public record class GetByIdChapterResultQuery(Guid Id): IRequest<GetSingleChapterResultDto>;
}
