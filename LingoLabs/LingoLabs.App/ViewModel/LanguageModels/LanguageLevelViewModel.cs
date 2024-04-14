using System.ComponentModel.DataAnnotations;
using LingoLabs.App.ViewModel.LanguageModels.EnrollmentModels;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class LanguageLevelViewModel
    {
        public Guid LanguageLevelId { get; set; }
        [Required(ErrorMessage = "LanguageLevel Name is required")]
        public string LanguageLevelName { get; set; } = string.Empty;
        [Required(ErrorMessage = "LanguageLevel Alias is required")]
        public string LanguageLevelAlias { get; set; } = string.Empty;
        [Required(ErrorMessage = "LanguageId is required")]
        public Guid? LanguageId { get; set; }
        public int? LanguageLevelPriorityNumber { get; set; }
        public string LanguageLevelDescription { get; set; } = string.Empty;
        public string LanguageLevelVideoLink { get; set; } = string.Empty;
        public List<ChoicerViewModel> LanguageChapters { get; set; } = [];
        public List<EntityTagViewModel> LanguageLeveKeyWords { get; set; } = [];
        public List<UserLanguageLevelViewModel> UserLanguageLevels { get; set; } = [];
    }
}
