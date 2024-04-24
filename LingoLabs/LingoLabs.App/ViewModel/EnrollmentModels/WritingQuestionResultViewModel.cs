using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.EnrollmentModels
{
    public class WritingQuestionResultViewModel : QuestionResultViewModel
    {
        public byte[] ImageData { get; set; } = [];
        public string RecognizedText { get; set; } = string.Empty;

        public bool IsValid
        {
            get
            {
                return (ImageData != null && ImageData.Length > 0) || !string.IsNullOrEmpty(RecognizedText);
            }
        }
    }
}
