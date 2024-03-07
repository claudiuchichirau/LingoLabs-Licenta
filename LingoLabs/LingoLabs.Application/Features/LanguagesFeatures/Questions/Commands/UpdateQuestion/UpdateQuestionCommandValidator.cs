﻿using FluentValidation;
using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandValidator: AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");

            RuleFor(p => p.UpdateQuestionDto.QuestionRequirement)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateQuestionDto.QuestionLearningType)
                .NotNull()
                .Must(type => type == LearningType.Auditory || type == LearningType.Visual || type == LearningType.Kinesthetic || type == LearningType.Logical)
                .WithMessage("{PropertyName} must have one of the following values: Auditory, Visual, Kinesthetic, Logical");

            RuleFor(p => p.UpdateQuestionDto.QuestionImageData)
                .Must(BeJpgOrPng).When(p => p.UpdateQuestionDto.QuestionImageData != null)
                .WithMessage("{PropertyName} should be a .jpg or .png image if it exists.");

            RuleFor(p => p.UpdateQuestionDto.QuestionVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateQuestionDto.QuestionVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.UpdateQuestionDto.QuestionVideoLink)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
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
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

    }
}
