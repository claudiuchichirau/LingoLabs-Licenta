using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.EnrollmentModels
{
    public class UserLanguageLevelViewModel
    {
        public Guid UserLanguageLevelId { get; set; }
        [Required(ErrorMessage = "EnrollmentId is required")]
        public Guid EnrollmentId { get; set; }
        [Required(ErrorMessage = "LanguageCompetenceId is required")]
        public Guid LanguageCompetenceId { get; set; }
        [Required(ErrorMessage = "LanguageLevelId is required")]
        public Guid LanguageLevelId { get; set; }
    }
}