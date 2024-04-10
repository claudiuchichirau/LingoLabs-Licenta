namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class UserQuizElement
    {
        public Guid LessonId { get; set; }
        public List<UserQuestionElement> UserQuestions { get; set; } = [];
    }
}
