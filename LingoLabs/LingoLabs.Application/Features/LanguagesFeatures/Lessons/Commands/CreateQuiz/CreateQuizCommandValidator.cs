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
                    if (questionList.Count >= 15)
                        return false;

                    foreach (var question in questionList)
                    {
                        if (string.IsNullOrEmpty(question.QuestionRequirement))
                            return false;

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
    }
}
