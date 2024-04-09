namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class LanguageLevelElement
    {
        public Guid LanguageLevelId { get; set; }
        public string LanguageLevelName { get; set; } = string.Empty;
        public string LanguageLevelAlias { get; set; } = string.Empty;
        public string? LanguageLevelDescription { get; set; }
        public string? LanguageLevelVideoLink { get; set; }
        public int? LanguageLevelPriorityNumber { get; set; }
        public List<ChapterElement> Chapters { get; set; }
    }
}
