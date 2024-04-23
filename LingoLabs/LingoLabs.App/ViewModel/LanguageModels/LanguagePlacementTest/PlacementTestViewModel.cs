using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.LanguagePlacementTest
{
    public class PlacementTestViewModel
    {
        [Required(ErrorMessage = "LanguageId is required")]
        public Guid LanguageId { get; set; }
        [Required(ErrorMessage = "Questions are required")]
        public List<Guid> QuestionsId { get; set; } = [];
    }
}
