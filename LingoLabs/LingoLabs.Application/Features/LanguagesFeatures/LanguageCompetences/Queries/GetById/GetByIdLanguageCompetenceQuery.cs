using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById
{
    public record class GetByIdLanguageCompetenceQuery(Guid Id): IRequest<GetSingleLanguageCompetenceDto>;
}
