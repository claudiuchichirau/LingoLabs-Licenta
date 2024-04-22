using LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.UpdateListeningLesson;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;
using System.Web;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, UpdateQuestionCommandResponse>
    {
        private readonly IQuestionRepository questionRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IChoiceRepository choiceRepository;

        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository, ILanguageRepository languageRepository, IChoiceRepository choiceRepository)
        {
            this.questionRepository = questionRepository;
            this.languageRepository = languageRepository;
            this.choiceRepository = choiceRepository;
        }
        public async Task<UpdateQuestionCommandResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuestionCommandValidator(languageRepository, questionRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var question = await questionRepository.FindByIdAsync(request.QuestionId);

            if (!question.IsSuccess)
            {
                return new UpdateQuestionCommandResponse 
                {
                    Success = false,
                    ValidationsErrors = new List<string> { question.Error }
                };
            }

            string newVideoLink = null;

            if (!string.IsNullOrEmpty(request.QuestionVideoLink))
            {
                string videoId = HttpUtility.ParseQueryString(new Uri(request.QuestionVideoLink).Query).Get("v");

                // Construct the new URL
                newVideoLink = $"https://www.youtube.com/embed/{videoId}";
            }

            question.Value.UpdateQuestion(
                request.QuestionRequirement,
                request.QuestionImageData,
                newVideoLink,
                request.LanguageId,
                request.QuestionPriorityNumber);

            await questionRepository.UpdateAsync(question.Value);

            foreach (var choice in question.Value.Choices)
            {
                var choiceResult = await choiceRepository.FindByIdAsync(choice.ChoiceId);

                if (!choiceResult.IsSuccess)
                {
                    return new UpdateQuestionCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { choiceResult.Error }
                    };
                }

                choiceResult.Value.UpdateChoice(choice.ChoiceContent, choice.IsCorrect);

                await choiceRepository.UpdateAsync(choiceResult.Value);
            }

            return new UpdateQuestionCommandResponse
            {
                Success = true,
                UpdateQuestion = new UpdateQuestionDto
                {
                    QuestionRequirement = question.Value.QuestionRequirement,
                    QuestionImageData = question.Value.QuestionImageData,
                    QuestionVideoLink = question.Value.QuestionVideoLink,
                    QuestionPriorityNumber = question.Value.QuestionPriorityNumber,
                }
            };
        }
    }
}
