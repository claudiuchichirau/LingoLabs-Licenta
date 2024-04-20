using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.LessonQuiz
{
    public class UpdateQuizViewModel
    {
        [Required(ErrorMessage = "LessonId is required")]
        public Guid LessonId { get; set; }
        [Required(ErrorMessage = "LessonId is required")]
        public List<QuestionViewModel> Questions { get; set; } = [];
    }
}
