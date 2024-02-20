using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Queries.GetAll
{
    public class GetAllWordPairsQueryHandler : IRequestHandler<GetAllWordPairsQuery, GetAllWordPairsResponse>
    {
        private readonly IWordPairRepository repository;

        public GetAllWordPairsQueryHandler(IWordPairRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllWordPairsResponse> Handle(GetAllWordPairsQuery request, CancellationToken cancellationToken)
        {
            GetAllWordPairsResponse response = new GetAllWordPairsResponse();
            var result = await repository.GetAllAsync();

            if(result.IsSuccess)
            {
                response.WordPairs = result.Value.Select(wordPair => new WordPairDto
                {
                    WordPairId = wordPair.WordPairId,
                    KeyWord = wordPair.KeyWord,
                    ValueWord = wordPair.ValueWord
                }).ToList();
            }
            return response;
        }
    }
}
