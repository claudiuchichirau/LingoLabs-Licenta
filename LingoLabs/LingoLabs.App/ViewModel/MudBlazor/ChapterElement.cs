namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class ChapterElement
    {
        public Guid ChapterId { get; set; }
        public string ChapterName { get; set; } = string.Empty;
        public string? ChapterDescription { get; set; } 
        public string? ChapterImageData { get; set; } 
        public string? ChapterVideoData { get; set; } 
        public int? ChapterPriorityNumber { get; set; }
        public List<LessonElement> Lessons { get; set; } = [];
    }
}
