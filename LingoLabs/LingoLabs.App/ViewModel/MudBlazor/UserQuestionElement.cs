namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class UserQuestionElement
    {
        public Guid QuestionId { get; set; }
        public QuestionTypeElement QuestionType { get; set; }
        public bool? ChoiceIsCorrect { get; set; }
        public string? ChoiceContent { get; set; }
        public Guid? ChoiceId { get; set; }
        public bool QuestionCompleted { get; set; }
    }
}
