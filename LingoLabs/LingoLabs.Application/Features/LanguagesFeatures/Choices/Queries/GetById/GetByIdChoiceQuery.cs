using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries.GetById
{
    public record class GetByIdChoiceQuery(Guid Id) : IRequest<ChoiceDto>;
}
