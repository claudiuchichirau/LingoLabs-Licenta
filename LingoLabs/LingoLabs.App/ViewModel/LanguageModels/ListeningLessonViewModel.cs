using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class ListeningLessonViewModel
    {
        public Guid LessonId { get; set; }
        [Required(ErrorMessage = "Lesson Title is required")]
        public string LessonTitle { get; set; } = string.Empty;
        [Required(ErrorMessage = "ChaperId is required")]
        public Guid ChapterId { get; set; }
        [Required(ErrorMessage = "TextScript is required")]
        public string TextScript { get; set; } = string.Empty;
        [Required(ErrorMessage = "Accents are required")]
        public List<AccentViewModel> Accents { get; set; } = [];
        [Required(ErrorMessage = "LanguageCompetenceId are required")]
        public Guid LanguageCompetenceId { get; set; }
        public int? LessonPriorityNumber { get; set; }
        public string LessonDescription { get; set; } = string.Empty;
        public string LessonContent { get; set; } = string.Empty;
        public string LessonRequirement { get; set; } = string.Empty;
        public string LessonVideoLink { get; set; } = string.Empty;
        public string LessonImageData { get; set; } = string.Empty;
        public string ChapterName { get; set; } = string.Empty;
        public string LanguageCompetenceName { get; set; } = string.Empty;
        public string LanguageLevelName { get; set; } = string.Empty;
        public List<QuestionViewModel> LessonQuestions { get; set; } = [];
        public List<EntityTagViewModel> LessonTags { get; set; } = [];
    }
}
