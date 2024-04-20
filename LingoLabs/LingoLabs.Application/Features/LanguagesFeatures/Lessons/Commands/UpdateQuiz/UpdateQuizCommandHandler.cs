﻿using LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries;
using LingoLabs.Application.Persistence.Languages;
using MediatR;
using System.Web;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, UpdateQuizCommandResponse>
    {
        private readonly ILessonRepository lessonRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IChoiceRepository choiceRepository;

        public UpdateQuizCommandHandler(ILessonRepository lessonRepository, IQuestionRepository questionRepository, IChoiceRepository choiceRepository)
        {
            this.lessonRepository = lessonRepository;
            this.questionRepository = questionRepository;
            this.choiceRepository = choiceRepository;
        }
        public async Task<UpdateQuizCommandResponse> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            Guid lessonId = request.LessonId;
            var validator = new UpdateQuizCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new UpdateQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var lesson = await lessonRepository.FindByIdAsync(request.LessonId);

            var updatedQuestions = new List<UpdateQuestionForDto>();

            foreach (var questionDto in request.Questions)
            {
                var question = await questionRepository.FindByIdAsync(questionDto.QuestionId);
                question.Value.UpdateQuestionRequirement(questionDto.QuestionRequirement);
                question.Value.UpdateQuestionType(questionDto.QuestionType);
                question.Value.UpdateQuestionImageData(questionDto.QuestionImageData);


                string newVideoLink = null;

                if (!string.IsNullOrEmpty(questionDto.QuestionVideoLink))
                {
                    string videoId = HttpUtility.ParseQueryString(new Uri(questionDto.QuestionVideoLink).Query).Get("v");

                    // Construct the new URL
                    newVideoLink = $"https://www.youtube.com/embed/{videoId}";
                }

                question.Value.UpdateQuestionVideoLink(newVideoLink);

                foreach (var choiceDto in questionDto.Choices)
                {
                    var choice = await choiceRepository.FindByIdAsync(choiceDto.ChoiceId);
                    choice.Value.UpdateContent(choiceDto.ChoiceContent);
                    choice.Value.UpdateCorrectness(choiceDto.IsCorrect);

                    var choiceResult = await choiceRepository.UpdateAsync(choice.Value);

                    if(!choiceResult.IsSuccess)
                    {
                        return new UpdateQuizCommandResponse
                        {
                            Success = false,
                            ValidationsErrors = [choiceResult.Error]
                        };
                    }
                }

                var questionResult = await questionRepository.UpdateAsync(question.Value);

                if(!questionResult.IsSuccess)
                {
                    return new UpdateQuizCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = [questionResult.Error]
                    };
                }

                var updatedQuestion = new UpdateQuestionForDto
                {
                    QuestionId = question.Value.QuestionId,
                    QuestionRequirement = question.Value.QuestionRequirement,
                    QuestionType = question.Value.QuestionType,
                    QuestionImageData = question.Value.QuestionImageData,
                    QuestionVideoLink = question.Value.QuestionVideoLink,
                    Choices = question.Value.Choices.Select(choice => new UpdateChoiceForDto
                    {
                        ChoiceId = choice.ChoiceId,
                        ChoiceContent = choice.ChoiceContent,
                        IsCorrect = choice.IsCorrect
                    }).ToList()
                };

                updatedQuestions.Add(updatedQuestion);
            }

            return new UpdateQuizCommandResponse
            {
                Success = true,
                UpdateQuiz = new UpdateQuizDto
                {
                    Questions = updatedQuestions
                }
            };
        }
    }
}
