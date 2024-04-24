using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.EnrollmentModels
{
    public class LanguageLevelResultViewModel
    {
        public Guid LanguageLevelResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid LanguageLevelId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid EnrollmentId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public bool IsCompleted { get; set; }
        public List<ChapterResultViewModel> ChapterResults { get; set; } = [];
    }
}
