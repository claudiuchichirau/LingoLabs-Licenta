using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.LessonQuiz
{
    public class ChoiceQuizViewModel
    {
        public Guid ChoiceId { get; set; }
        [Required(ErrorMessage = "ChoiceContent is required")]
        public string ChoiceContent { get; set; } = string.Empty;
        [Required(ErrorMessage = "IsCorrect is required")]
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
    }
}