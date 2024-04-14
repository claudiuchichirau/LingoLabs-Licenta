using LingoLabs.App.ViewModel.LanguageModels.EnrollmentModels;
using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class LanguageCompetenceViewModel
    {
        public Guid LanguageCompetenceId { get; set; }
        [Required(ErrorMessage = "LanguageCompetence Name is required")]
        public string LanguageCompetenceName { get; set; } = string.Empty;
        [Required(ErrorMessage = "LanguageCompetenceType is required")]
        public LanguageCompetenceTypeViewModel LanguageCompetenceType { get; set; }
        [Required(ErrorMessage = "LanguageId is required")]
        public Guid LanguageId { get; set; }
        public int? LanguageCompetencePriorityNumber { get; set; }
        public string LanguageCompetenceDescription { get; set; } = string.Empty;
        public string LanguageCompetenceVideoLink { get; set; } = string.Empty;
        public List<TagViewModel> LearningCompetenceKeyWords { get; set; } = [];
        public List<UserLanguageLevelViewModel> UserLanguageLevels { get; set; } = [];
        public List<LessonViewModel> LanguageCompetenceLessons { get; set; } = [];
    }
}
