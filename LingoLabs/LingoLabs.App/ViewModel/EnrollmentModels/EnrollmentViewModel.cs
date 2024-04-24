using LingoLabs.App.ViewModel.LanguageModels.EnrollmentModels;
using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.EnrollmentModels
{
    public class EnrollmentViewModel
    {
        public Guid EnrollmentId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid LanguageId { get; set; }
        public List<LanguageLevelResultViewModel> LanguageLevelResults { get; set; } = [];
        public List<LanguageCompetenceResultViewModel> LanguageCompetenceResults { get; set; } = [];
        public List<UserLanguageLevelViewModel> UserLanguageLevels { get; set; } = [];
    }
}
