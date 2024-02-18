using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Commands.CreateMatchingWordsQuestion
{
    public class CreateMatchingWordsQuestionCommandHandler: IRequestHandler<CreateMatchingWordsQuestionCommand, CreateMatchingWordsQuestionCommandResponse>
    {
        private readonly IMatchingWordsQuestionRepository repository;

        public CreateMatchingWordsQuestionCommandHandler(IMatchingWordsQuestionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateMatchingWordsQuestionCommandResponse> Handle(CreateMatchingWordsQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMatchingWordsQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateMatchingWordsQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var matchingWordsQuestion = MatchingWordsQuestion.Create(request.QuestionRequirement, request.QuestionLearningType, request.WordPairs);

            if (matchingWordsQuestion.IsSuccess)
            {
                await repository.AddAsync(matchingWordsQuestion.Value);
                return new CreateMatchingWordsQuestionCommandResponse
                {
                    MatchingWordsQuestion = new CreateMatchingWordsQuestionDto
                    {
                        QuestionId = matchingWordsQuestion.Value.QuestionId,
                        QuestionRequirement = matchingWordsQuestion.Value.QuestionRequirement,
                        QuestionLearningType = matchingWordsQuestion.Value.QuestionLearningType,
                        WordPairs = matchingWordsQuestion.Value.WordPairs
                    }
                };
            }

            return new CreateMatchingWordsQuestionCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { matchingWordsQuestion.Error }
            };
        }
    }
}
