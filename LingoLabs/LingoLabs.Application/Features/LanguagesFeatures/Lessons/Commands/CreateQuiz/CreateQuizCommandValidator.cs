using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class CreateQuizCommandValidator: AbstractValidator<CreateQuizCommand>
    {
        public CreateQuizCommandValidator()
        {
            RuleFor(p => p.LessonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");

            RuleFor(p => p.QuestionList)
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
        }
    }
}
