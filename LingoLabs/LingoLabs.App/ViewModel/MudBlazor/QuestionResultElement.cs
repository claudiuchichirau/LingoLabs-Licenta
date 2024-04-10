using System.Security.Cryptography.X509Certificates;

namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class QuestionResultElement
    {
        public Guid QuestionId { get; set; }
        public QuestionTypeElement QuestionType { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public Guid? GrilaCorrectChoiceId { get; set; }
        public Guid? GrilaUserChoiceId { get; set; }
        public bool? TrueFalseCorrectValue { get; set; }
        public bool? TrueFalseUserValue { get; set; }
        public List<string>? CuvantLipsaCorrectValues { get; set; } = [];
        public string? CuvantLipsaUserValue { get; set; } = string.Empty;
        public bool UserAnswerIsCorrect { get; set; }
    }
}
