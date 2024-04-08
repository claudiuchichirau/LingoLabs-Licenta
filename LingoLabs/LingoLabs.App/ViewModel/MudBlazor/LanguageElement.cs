namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class LanguageElement
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; } = string.Empty;
        public string LanguageDescription { get; set; } = string.Empty;
        public string LanguageVideoLink { get; set; } = string.Empty;
        public string LanguageFlag { get; set; } = string.Empty;
        public List<LanguageLevelElement> LanguageLevels { get; set; } = [];
        public List<LanguageCompetenceElement> LanguageCompetences { get; set; } = [];
        public List<QuestionElement> PlacementTest { get; set; } = [];
        //public List<Tag> LanguageKeyWords { get; set; } = [];
    }
}
