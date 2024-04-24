using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.EnrollmentModels
{
    public class LanguageCompetenceResultViewModel
    {
        public Guid LanguageCompetenceResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid LanguageCompetenceId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid EnrollmentId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public bool IsCompleted { get; set; }
        public List<LessonResultViewModel> LessonResults { get; set; } = [];
    }
}
