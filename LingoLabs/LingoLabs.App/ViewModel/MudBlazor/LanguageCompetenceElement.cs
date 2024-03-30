namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class LanguageCompetenceElement
    {
        public string LanguageCompetenceName { get; set; } = string.Empty;
        public LanguageCompetenceTypeElement LanguageCompetenceType { get; set; }
        public string? LanguageCompetenceDescription { get; set; }
        public string? LanguageCompetenceVideoLink { get; set; }
        public int? LanguageCompetencePriorityNumber { get; set; }
    }
}
