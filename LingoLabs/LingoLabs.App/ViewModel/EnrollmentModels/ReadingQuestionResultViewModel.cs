using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.EnrollmentModels
{
    public class ReadingQuestionResultViewModel : QuestionResultViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public byte[] AudioData { get; set; } = [];
        public string RecognizedText { get; set; } = string.Empty;
    }
}
