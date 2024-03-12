using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeletePlacementTest
{
    public class DeletePlacementTestCommandHandler : IRequestHandler<DeletePlacementTestCommand, DeletePlacementTestCommandResponse>
    {
        private readonly ILanguageRepository languageRepository;
        private readonly IQuestionRepository questionRepository;

        public DeletePlacementTestCommandHandler(ILanguageRepository languageRepository, IQuestionRepository questionRepository)
        {
            this.languageRepository = languageRepository;
            this.questionRepository = questionRepository;
        }
        public async Task<DeletePlacementTestCommandResponse> Handle(DeletePlacementTestCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeletePlacementTestCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new DeletePlacementTestCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var language = await languageRepository.FindByIdAsync(request.LanguageId);

            if(!language.IsSuccess)
            {
                return new DeletePlacementTestCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { language.Error }
                };
            }

            List<Question> placementTestQuestions = new List<Question>(language.Value.PlacementTest);

            foreach (var question in placementTestQuestions)
            {
                var questionExists = await questionRepository.FindByIdAsync(question.QuestionId);

                if(!questionExists.IsSuccess)
                {
                    return new DeletePlacementTestCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { questionExists.Error }
                    };
                }

                questionExists.Value.RemoveQuestionLanguageId();

                var updateQuestion = await questionRepository.UpdateAsync(questionExists.Value);

                if(!updateQuestion.IsSuccess)
                {
                    return new DeletePlacementTestCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { updateQuestion.Error }
                    };
                }
            }

            return new DeletePlacementTestCommandResponse
            {
                Success = true
            };

        }
    }
}
