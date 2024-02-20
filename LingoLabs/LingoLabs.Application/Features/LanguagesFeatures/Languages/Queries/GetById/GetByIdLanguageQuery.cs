using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries.GetById
{
    public record class GetByIdLanguageQuery(Guid Id) : IRequest<GetSingleLanguageDto>;
}
