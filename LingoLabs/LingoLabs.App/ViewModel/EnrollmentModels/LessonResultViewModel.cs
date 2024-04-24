using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.EnrollmentModels
{
    public class LessonResultViewModel
    {
        public Guid LessonResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid LessonId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid ChapterResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid LanguageCompetenceResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public bool IsCompleted { get; set; }
        public List<QuestionResultViewModel> QuestionResults { get; set; } = [];
    }
}
