using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.LessonQuiz
{
    public class QuizViewModel
    {
        [Required(ErrorMessage = "LessonId is required")]
        public Guid LessonId { get; set; }
        [Required(ErrorMessage = "LessonId is required")]
        public List<QuestionQuizViewModel> QuestionList { get; set; } = [];
    }
}
