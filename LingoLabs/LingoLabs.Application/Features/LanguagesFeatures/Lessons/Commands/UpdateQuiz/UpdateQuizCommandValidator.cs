using FluentValidation;
using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class UpdateQuizCommandValidator: AbstractValidator<UpdateQuizCommand>
    {
        public UpdateQuizCommandValidator()
        {
            RuleFor(p => p.LessonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");

            RuleFor(p => p.UpdateQuizDto)
                .Must((questionList) =>
                {
                    if (questionList.Count >= 15)
                        return false;

                    foreach (var question in questionList)
                    {
                        if (string.IsNullOrEmpty(question.QuestionRequirement) || question.QuestionRequirement.Length >= 500)
                            return false;

                        if (question.QuestionLearningType != LearningType.Auditory && question.QuestionLearningType != LearningType.Visual && question.QuestionLearningType != LearningType.Kinesthetic && question.QuestionLearningType != LearningType.Logical)
                            return false;

                        if(question.QuestionImageData != null && question.QuestionImageData.Length > 0)
                        {
                            return BeJpgOrPng(question.QuestionImageData);
                        }

                        if (!string.IsNullOrEmpty(question.QuestionVideoLink))
                        {
                            return BeValidUrl(question.QuestionVideoLink);
                        }

                        int goodAnswersCount = 0;
                        foreach (var choice in question.Choices)
                        {
                            if (string.IsNullOrEmpty(choice.ChoiceContent))
                                return false;
                            if (choice.IsCorrect)
                                goodAnswersCount++;
                        }

                        if (goodAnswersCount != 1)
                            return false;
                    }

                    return true;
                }).WithMessage("Maximum of 15 questions and 1 correct answer per question is allowed.");
        }

        private bool BeJpgOrPng(byte[] imageData)
        {
            var jpgHeader = new byte[] { 0xFF, 0xD8 };
            var pngHeader = new byte[] { 0x89, 0x50, 0x4E, 0x47 };

            if (imageData.Take(2).SequenceEqual(jpgHeader) || imageData.Take(4).SequenceEqual(pngHeader))
            {
                return true;
            }

            return false;
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
               && (uriResult.Host == "www.youtube.com" || uriResult.Host == "youtu.be");
        }
    }
}
