using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class LessonViewModel
    {
        public Guid LessonId { get; set; }
        [Required(ErrorMessage = "Lesson Title is required")]
        public string LessonTitle { get; set; } = string.Empty;
        [Required(ErrorMessage = "ChaperId is required")]
        public Guid ChapterId { get; set; }
        [Required(ErrorMessage = "LanguageCompetenceId is required")]
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
