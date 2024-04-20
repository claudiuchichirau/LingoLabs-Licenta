using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class QuestionViewModel
    {
        public Guid QuestionId { get; set; }
        [Required(ErrorMessage = "Question Requirement is required")]
        public string QuestionRequirement { get; set; } = string.Empty;
        [Required(ErrorMessage = "QuestionType is required")]
        public QuestionTypeViewModel QuestionType { get; set; }
        [Required(ErrorMessage = "LessonId is required")]
        public Guid LessonId { get; set; }
        public int? QuestionPriorityNumber { get; set; }
        public string QuestionImageData { get; set; } = string.Empty;
        public string QuestionVideoLink { get; set; } = string.Empty;
        public List<ChoiceViewModel> Choices { get; set; } = [];
        public Guid? LanguageId { get; set; }
    }
}