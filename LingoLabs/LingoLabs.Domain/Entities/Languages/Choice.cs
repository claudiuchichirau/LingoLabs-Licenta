using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Choice : AuditableEntity
    {
        public Guid ChoiceId { get; private set; }
        public string ChoiceContent { get; private set; }
        public bool IsCorrect { get; private set; }
        public Guid QuestionId { get; private set; }
        public Question? Question { get; set; }

        private Choice(string choiceContent, bool isCorrect, Guid questionId)
        {
            ChoiceId = Guid.NewGuid();
            ChoiceContent = choiceContent;
            IsCorrect = isCorrect;
            QuestionId = questionId;
        }

        public static Result<Choice> Create(string choiceContent, bool isCorrect, Guid questionId)
        {
            if (string.IsNullOrWhiteSpace(choiceContent))
                return Result<Choice>.Failure("ChoiceContent is required");
            if (questionId == default)
                return Result<Choice>.Failure("QuestionId is required");

            return Result<Choice>.Success(new Choice(choiceContent, isCorrect, questionId));
        }

        public void UpdateChoice(string choiceContent, bool isCorrect)
        {
            ChoiceContent = choiceContent;
            IsCorrect = isCorrect;
        }

        public void UpdateContent(string choiceContent)
        {
            ChoiceContent = choiceContent;
        }

        public void UpdateCorrectness(bool isCorrect)
        {
            IsCorrect = isCorrect;
        }
    }
}
