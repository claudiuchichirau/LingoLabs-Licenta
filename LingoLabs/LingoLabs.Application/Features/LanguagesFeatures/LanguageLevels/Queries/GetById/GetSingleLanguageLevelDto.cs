using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetById
{
    public class GetSingleLanguageLevelDto: LanguageLevelDto
    {
        public List<ChapterDto> LanguageChapters { get; set; } = [];
        public List<TagDto> LanguageLeveKeyWords { get; set; } = [];
        public List<TagDto> UnassociatedTags { get; set; } = [];
        public List<UserLanguageLevelDto> UserLanguageLevels { get; set; } = [];
        public string LanguageName { get; set; } = string.Empty;
    }
}
