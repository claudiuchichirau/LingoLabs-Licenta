using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel;
using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreatePlacementTest;
using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeletePlacementTest;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdatePlacementTest
{
    public class UpdatePlacementTestCommandHandler : IRequestHandler<UpdatePlacementTestCommand, UpdatePlacementTestCommandResponse>
    {
        private readonly ILanguageRepository languageRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly DeletePlacementTestCommandHandler deletePlacementTestCommandHandler;

        public UpdatePlacementTestCommandHandler(ILanguageRepository languageRepository, IQuestionRepository questionRepository, DeletePlacementTestCommandHandler deletePlacementTestCommandHandler)
        {
            this.languageRepository = languageRepository;
            this.questionRepository = questionRepository;
            this.deletePlacementTestCommandHandler = deletePlacementTestCommandHandler;
        }
        public async Task<UpdatePlacementTestCommandResponse> Handle(UpdatePlacementTestCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePlacementTestCommandValidator(questionRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdatePlacementTestCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var language = await languageRepository.FindByIdAsync(request.LanguageId);

            if (!language.IsSuccess)
            {
                return new UpdatePlacementTestCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { language.Error }
                };
            }

            var deletePlacementTestCommand = new DeletePlacementTestCommand { LanguageId = request.LanguageId };
            var deletePlacementTestCommandResponse = await deletePlacementTestCommandHandler.Handle(deletePlacementTestCommand, cancellationToken);

            if (!deletePlacementTestCommandResponse.Success)
            {
                return new UpdatePlacementTestCommandResponse
                {
                    Success = false,
                    ValidationsErrors = deletePlacementTestCommandResponse.ValidationsErrors
                };
            }

            List<CreatePlacementTestQuestionDto> questions = [];

            foreach (var question in request.Questions)
            {
                var questionExists = await questionRepository.FindByIdAsync(question.QuestionId);

                if (!questionExists.IsSuccess)
                {
                    return new UpdatePlacementTestCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { questionExists.Error }
                    };
                }

                var questionDto = new CreatePlacementTestQuestionDto
                {
                    QuestionId = question.QuestionId
                };

                questions.Add(questionDto);

                questionExists.Value.UpdateQuestionLanguageId(request.LanguageId);

                var updateResult = await questionRepository.UpdateAsync(questionExists.Value);

                if (!updateResult.IsSuccess)
                {
                    return new UpdatePlacementTestCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { updateResult.Error }
                    };
                }
            }

            return new UpdatePlacementTestCommandResponse
            {
                Success = true,
                UpdatePlacementTestDto = new UpdatePlacementTestDto
                {
                    Questions = questions
                }
            };

        }
    }
}
