using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries.GetById
{
    public class GetByIdChoiceQueryHandler : IRequestHandler<GetByIdChoiceQuery, ChoiceDto>
    {
        private readonly IChoiceRepository repository;

        public GetByIdChoiceQueryHandler(IChoiceRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ChoiceDto> Handle(GetByIdChoiceQuery request, CancellationToken cancellationToken)
        {
            var choice = await repository.FindByIdAsync(request.Id);
            if(choice.IsSuccess)
            {
                return new ChoiceDto
                {
                    ChoiceId = choice.Value.ChoiceId,
                    ChoiceContent = choice.Value.ChoiceContent,
                    IsCorrect = choice.Value.IsCorrect,
                    QuestionId = choice.Value.QuestionId
                };
            }

            return new ChoiceDto();
        }
    }
}
