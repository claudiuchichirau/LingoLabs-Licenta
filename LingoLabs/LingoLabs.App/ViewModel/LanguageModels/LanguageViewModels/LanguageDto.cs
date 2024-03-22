namespace LingoLabs.App.ViewModel.LanguageModels.LanguageViewModels
{
    public class LanguageDto
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; } = string.Empty;
        public string LanguageDescription { get; set; } = string.Empty;
        public string LanguageVideoLink { get; set; } = string.Empty;
        public byte[] LanguageFlag { get; set; } = [];

        //public List<LanguageLevelDto> LanguageLevels { get; set; } = [];
        //public List<LanguageCompetenceDto> LanguageCompetences { get; set; } = [];
        //public List<QuestionDto> PlacementTest { get; set; } = [];
        //public List<TagDto> LanguageKeyWords { get; set; } = [];
        //public List<TagDto> UnassociatedTags { get; set; } = [];
    }
}
