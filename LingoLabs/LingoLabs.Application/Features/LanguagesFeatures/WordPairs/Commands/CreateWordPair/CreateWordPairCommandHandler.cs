using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Commands.CreateWordPair
{
    public class CreateWordPairCommandHandler: IRequestHandler<CreateWordPairCommand, CreateWordPairCommandResponse>
    {
        private readonly IWordPairRepository repository;

        public CreateWordPairCommandHandler(IWordPairRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateWordPairCommandResponse> Handle(CreateWordPairCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateWordPairCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateWordPairCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var wordPair = WordPair.Create(request.KeyWord, request.ValueWord);
            if (wordPair.IsSuccess)
            {
                await repository.AddAsync(wordPair.Value);
                return new CreateWordPairCommandResponse
                {
                    WordPair = new CreateWordPairDto
                    {
                        WordPairId = wordPair.Value.WordPairId,
                        KeyWord = wordPair.Value.KeyWord,
                        ValueWord = wordPair.Value.ValueWord
                    }
                };
            }

            return new CreateWordPairCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { wordPair.Error }
            };
        }
    }
}
