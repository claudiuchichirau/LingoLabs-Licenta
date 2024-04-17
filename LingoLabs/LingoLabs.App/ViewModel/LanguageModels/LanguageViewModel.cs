using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class LanguageViewModel
    {
        public Guid LanguageId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} must not exceed 50 characters.")]
        public string LanguageName { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        public string LanguageFlag { get; set; } = string.Empty;

        public string LanguageDescription { get; set; } = string.Empty;
        public string LanguageVideoLink { get; set; } = string.Empty;
        public int LanguageLevelCount { get; set; }
        public int LessonCount { get; set; }
        public List<LanguageLevelViewModel> LanguageLevels { get; set; } = [];
        public List<LanguageCompetenceViewModel> LanguageCompetences { get; set; } = [];
        public List<QuestionViewModel> PlacementTest { get; set; } = [];
        public List<EntityTagViewModel> LanguageKeyWords { get; set; } = [];
    }
}
