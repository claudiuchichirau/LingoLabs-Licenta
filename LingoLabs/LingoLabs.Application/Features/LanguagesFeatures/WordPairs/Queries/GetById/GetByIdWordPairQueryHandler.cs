using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Queries.GetById
{
    public class GetByIdWordPairQueryHandler : IRequestHandler<GetByIdWordPairQuery, GetSingleWordPairDto>
    {
        private readonly IWordPairRepository repository;

        public GetByIdWordPairQueryHandler(IWordPairRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetSingleWordPairDto> Handle(GetByIdWordPairQuery request, CancellationToken cancellationToken)
        {
            var wordPair = await repository.FindByIdAsync(request.Id);
            if(wordPair.IsSuccess)
            {
                return new GetSingleWordPairDto
                {
                    WordPairId = wordPair.Value.WordPairId,
                    KeyWord = wordPair.Value.KeyWord,
                    ValueWord = wordPair.Value.ValueWord,
                    MatchingWordsQuestionId = wordPair.Value.MatchingWordsQuestionId
                };
            }

            return new GetSingleWordPairDto();
        }
    }
}
