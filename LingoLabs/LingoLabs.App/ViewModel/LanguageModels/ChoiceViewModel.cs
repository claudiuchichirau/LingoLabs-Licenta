using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class ChoiceViewModel
    {
        public Guid ChoiceId { get; set; }
        [Required(ErrorMessage = "Choice Content is required")]
        public string ChoiceContent { get; set; } = string.Empty;
        [Required(ErrorMessage = "IsCorrect is required")]
        public bool IsCorrect { get; set; }
        [Required(ErrorMessage = "QuestionId is required")]
        public Guid QuestionId { get; set; }
    }
}