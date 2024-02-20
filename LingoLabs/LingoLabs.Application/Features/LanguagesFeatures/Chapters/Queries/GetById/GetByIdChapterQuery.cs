using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries.GetById
{
    public record class GetByIdChapterQuery(Guid Id): IRequest<GetSingleChapterDto>;
}
