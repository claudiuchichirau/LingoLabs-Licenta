using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.EnrollmentModels
{
    public class ChapterResultViewModel
    {
        public Guid ChapterResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid ChapterId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid LanguageLevelResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public bool IsCompleted { get; set; }
        public List<LessonResultViewModel> LessonResults { get; set; } = [];
    }
}
