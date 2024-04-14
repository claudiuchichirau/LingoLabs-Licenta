using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.LessonQuiz
{
    public class ChoiceQuizViewModel
    {
        [Required(ErrorMessage = "ChoiceContent is required")]
        public string ChoiceContent { get; set; } = string.Empty;
        [Required(ErrorMessage = "IsCorrect is required")]
        public bool IsCorrect { get; set; }
    }
}