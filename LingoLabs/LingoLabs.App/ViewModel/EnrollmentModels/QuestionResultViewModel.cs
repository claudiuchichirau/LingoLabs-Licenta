using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.EnrollmentModels
{
    public class QuestionResultViewModel
    {
        public Guid QuestionResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid QuestionId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid LessonResultId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public bool IsCorrect { get; set; }
    }
}
