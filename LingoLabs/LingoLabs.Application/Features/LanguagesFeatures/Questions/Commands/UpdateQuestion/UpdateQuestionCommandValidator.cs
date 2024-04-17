using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities;
using System.Net;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandValidator: AbstractValidator<UpdateQuestionCommand>
    {
        private readonly ILanguageRepository languageRepository;
        private readonly IQuestionRepository questionRepository;
        public UpdateQuestionCommandValidator(ILanguageRepository languageRepository, IQuestionRepository questionRepository)
        {
            this.languageRepository = languageRepository;
            this.questionRepository = questionRepository;

            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");

            RuleFor(p => p.QuestionRequirement)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.QuestionImageData)
                .Must(BeImageValidUrl).When(p => !string.IsNullOrEmpty(p.QuestionImageData))
                .WithMessage("{PropertyName} should be a .jpg or .png image if it exists.");

            RuleFor(p => p.QuestionVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.QuestionVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.LanguageId)
                .MustAsync(async (languageId, cancellation) =>
                {
                    if (languageId == Guid.Empty)
                        return true;

                    var language = await languageRepository.FindByIdAsync(languageId);
                    return language.IsSuccess;
                })
                .WithMessage("LanguageId must exist.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidatePriorityNumber(p.QuestionPriorityNumber.Value, p.QuestionId, questionRepository))
                .When(p => p.QuestionPriorityNumber.HasValue && p.QuestionPriorityNumber.Value > 0)
                .WithMessage("PriorityNumber must be unique.");

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

        private async Task<bool> ValidatePriorityNumber(int priorityNumber, Guid questionId, IQuestionRepository questionRepository)
        {
            if (await questionRepository.ExistsQuestionPriorityNumberAsync(priorityNumber, questionId) == true)
                return false;
            return true;
        }

    }
}
