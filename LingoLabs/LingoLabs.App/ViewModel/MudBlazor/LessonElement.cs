namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class LessonElement
    {
        public Guid LessonId { get; set; }
        public string LessonTitle{ get; set; } = string.Empty;
        public Guid ChapterId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
        public string? LessonDescription { get; set; }
        public string? LessonRequirement { get; set; }
        public string? LessonContent { get; set; }
        public string? LessonVideoLink { get; set; }
        public string? LessonImageLink { get; set; }
        public int? LessonPriorityNumber { get; set; }
        // List Tags
        // List questions
    }
}
