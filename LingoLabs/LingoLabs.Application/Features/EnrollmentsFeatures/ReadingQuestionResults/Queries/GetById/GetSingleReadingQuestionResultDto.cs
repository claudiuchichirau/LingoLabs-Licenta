namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Queries.GetById
{
    public class GetSingleReadingQuestionResultDto: ReadingQuestionResultDto
    {
        public byte[] AudioData { get; set; } = [];
        public string RecognizedText { get; set; } = string.Empty;
    }
}
