using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.LanguagePlacementTest
{
    public class QuestionPlacementTestViewModel
    {
        [Required(ErrorMessage = "QuestionId are required")]
        public Guid QuestionId { get; set; }
    }
}