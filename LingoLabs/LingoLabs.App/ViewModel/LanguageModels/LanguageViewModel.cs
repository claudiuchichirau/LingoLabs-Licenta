using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class LanguageViewModel
    {
        public Guid LanguageId { get; set; }
        [Required(ErrorMessage = "Language Name is required")]
        public string LanguageName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Language Flag is required")]
        public string LanguageFlag { get; set; } = string.Empty;

        public string LanguageDescription { get; set; } = string.Empty;
        public string LanguageVideoLink { get; set; } = string.Empty;
        public List<LanguageLevelViewModel> LanguageLevels { get; set; } = [];
        public List<LanguageCompetenceViewModel> LanguageCompetences { get; set; } = [];
        public List<QuestionViewModel> PlacementTest { get; set; } = [];
        public List<EntityTagViewModel> LanguageKeyWords { get; set; } = [];
    }
}
