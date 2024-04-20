using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.LessonQuiz
{
    public class QuestionQuizViewModel
    {
        public Guid QuestionId { get; set; }
        [Required(ErrorMessage = "QuestionRequirement is required")]
        public string QuestionRequirement { get; set; } = string.Empty;
        [Required(ErrorMessage = "QuestionType is required")]
        public QuestionTypeViewModel QuestionType { get; set; }
        [Required(ErrorMessage = "Choices are required")]
        public List<ChoiceQuizViewModel> Choices { get; set; } = [];
        public Guid LessonId { get; set; }
        public int? QuestionPriorityNumber { get; set; }
        public string QuestionImageData { get; set; } = string.Empty;
        public string QuestionVideoLink { get; set; } = string.Empty;
        public Guid? LanguageId { get; set; }

    }
}