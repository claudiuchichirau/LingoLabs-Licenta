﻿using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandValidator: AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionCommandValidator()
        {
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
