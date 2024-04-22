using FluentValidation;
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

            RuleFor(p => p.Questions)
                .Must((questionList) =>
                {
                    if (questionList.Count < 10)
                        return false;

                    foreach (var question in questionList)
                    {
                        if (string.IsNullOrEmpty(question.QuestionRequirement))
                            return false;

                        if (question.QuestionType == Domain.Entities.Languages.QuestionType.MultipleChoice)
                        {
                            if (question.Choices.Count < 3)
                                return false;

                            int goodAnswersCount = 0;
                            foreach (var choice in question.Choices)
                            {
                                if (choice.IsCorrect)
                                    goodAnswersCount++;
                            }

                            if (goodAnswersCount != 1)
                                return false;
                        }

                        if (question.QuestionType == Domain.Entities.Languages.QuestionType.TrueFalse)
                        {
                            if (question.Choices.Count != 1)
                                return false;
                        }

                        if (question.QuestionType == Domain.Entities.Languages.QuestionType.MissingWord)
                        {
                            if (question.Choices.Count < 1)
                                return false;
                        }
                    }

                    return true;
                }).WithMessage("The question list must contain more than 10 questions. Each question must have a requirement. For multiple-choice questions, there must be exactly one correct answer. For true/false questions, there must be exactly two answer options. For fill-in-the-blank questions, there must be exactly one answer.");

            RuleFor(p => p.Questions)
                .Must((questionList) =>
                {
                    foreach (var question in questionList)
                    {
                        if (!string.IsNullOrEmpty(question.QuestionImageData) && !BeImageValidUrl(question.QuestionImageData))
                            return false;
                    }

                    return true;
                }).WithMessage("The image link should be valid if it exists.");

            RuleFor(p => p.Questions)
                .Must((questionList) =>
                {
                    foreach (var question in questionList)
                    {
                        if (!string.IsNullOrEmpty(question.QuestionVideoLink) && !BeValidUrl(question.QuestionVideoLink))
                            return false;
                    }

                    return true;
                }).WithMessage("The image link should be valid if it exists.");
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
                try
                {
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        return response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase);
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse errorResponse)
                    {
                        // Handle specific HTTP error codes here, if needed
                    }
                    return false;
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
