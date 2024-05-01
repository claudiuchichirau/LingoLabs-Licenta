using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class ChapterViewModel
    {
        public Guid ChapterId { get; set; }
        [Required(ErrorMessage = "Chapter Name is required")]
        public string ChapterName { get; set; } = string.Empty;
        [Required(ErrorMessage = "LanguageLevelId is required")]
        public Guid LanguageLevelId { get; set; }
        public int? ChapterPriorityNumber { get; set; }
        public string ChapterDescription { get; set; } = string.Empty;
        public string ChapterImageData { get; set; } = string.Empty;
        public string ChapterVideoLink { get; set; } = string.Empty;
        public List<ListeningLessonViewModel>? ChapterLessons { get; set; } = [];
        public List<EntityTagViewModel> ChapterKeyWords { get; set; } = [];
        public string LanguageLevelName { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
