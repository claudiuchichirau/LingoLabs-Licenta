namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class ListeningLessonElement : LessonElement
    {
        public string TextScript { get; set; } = string.Empty;
        public List<AccentElement> Accents { get; set; } = [];
    }
}
