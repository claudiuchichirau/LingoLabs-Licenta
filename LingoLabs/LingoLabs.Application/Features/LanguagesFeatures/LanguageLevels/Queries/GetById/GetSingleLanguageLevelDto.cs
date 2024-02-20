using LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetById
{
    public class GetSingleLanguageLevelDto: LanguageLevelDto
    {
        public string LanguageLevelDescription { get; set; } = string.Empty;
        public string LanguageLevelVideoLink { get; set; } = string.Empty;
        public List<ChapterDto> LanguageChapters { get; set; } = [];
        public List<TagDto> LanguageLeveKeyWords { get; set; } = [];
    }
}
