namespace LingoLabs.App.ViewModel.LanguageModels.LanguagePlacementTest
{
    public class UserQuestionResponseViewModel
    {
        public Guid QuestionId { get; set; }
        public QuestionTypeViewModel QuestionType { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public bool? ChoiceUserIsCorrect { get; set; }
        public string? ChoiceContent { get; set; }
        public Guid? UserChoiceId { get; set; }
        public bool QuestionCompleted { get; set; }
    }
}
