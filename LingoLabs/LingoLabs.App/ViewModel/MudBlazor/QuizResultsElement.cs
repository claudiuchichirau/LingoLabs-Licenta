namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class QuizResultsElement
    {
        public Guid LessonId { get; set; }
        public List<QuestionResultElement> questionResultElements { get; set; } = [];
    }
}
