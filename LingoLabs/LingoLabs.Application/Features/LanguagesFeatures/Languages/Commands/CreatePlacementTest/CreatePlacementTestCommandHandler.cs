using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreatePlacementTest
{
    public class CreatePlacementTestCommandHandler: IRequestHandler<CreatePlacementTestCommand, CreatePlacementTestCommandResponse>
    {
        private readonly ILanguageRepository languageRepository;
        private readonly IQuestionRepository questionRepository;

        public CreatePlacementTestCommandHandler(ILanguageRepository languageRepository, IQuestionRepository questionRepository)
        {
            this.languageRepository = languageRepository;
            this.questionRepository = questionRepository;
        }

        public async Task<CreatePlacementTestCommandResponse> Handle(CreatePlacementTestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePlacementTestCommandValidator(questionRepository, languageRepository);
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                return new CreatePlacementTestCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var language = await languageRepository.FindByIdAsync(request.LanguageId);
            
            if(!language.IsSuccess)
            {
                return new CreatePlacementTestCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { language.Error }
                };
            }

            List<CreatePlacementTestQuestionDto> questions = [];

            foreach (var question in request.Questions)
            {
                var questionExists = await questionRepository.FindByIdAsync(question.QuestionId);

                if(!questionExists.IsSuccess)
                {
                    return new CreatePlacementTestCommandResponse
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

                if(!updateResult.IsSuccess)
                {
                    return new CreatePlacementTestCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { updateResult.Error }
                    };
                }
            }

            return new CreatePlacementTestCommandResponse
            {
                Success = true,
                CreatePlacementTestDto = new CreatePlacementTestDto
                {
                    Questions = questions
                }
            };
        }
    }
}
