namespace LingoLabs.Application.Features.EnrollmentsFeatures.WritingQuestionResults.Queries.GetById
{
    public class GetSingleWritingQuestionResultDto: WritingQuestionResultDto
    {
        public byte[] ImageData { get; set; } = [];
        public string RecognizedText { get; set; } = string.Empty;
    }
}
