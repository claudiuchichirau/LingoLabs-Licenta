using FluentValidation;
using LingoLabs.Domain.Entities;
using LingoLabs.Domain.Entities.Languages;
using System.Net;

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

                        if (question.QuestionType != QuestionType.MultipleChoice && question.QuestionType != QuestionType.TrueFalse && question.QuestionType != QuestionType.MissingWord)
                            return false;

                        if(question.QuestionImageData != null && question.QuestionImageData.Length > 0)
                        {
                            return BeImageValidUrl(question.QuestionImageData);
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

        private static bool BeImageValidUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result)
            {
                var request = WebRequest.Create(uriResult) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    return response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase);
                }
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
