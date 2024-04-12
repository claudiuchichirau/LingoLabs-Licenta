using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries.GetById
{
    public class GetSingleChapterDto: ChapterDto
    {
        public List<TagDto> ChapterKeyWords { get; set; } = [];
        public List<TagDto> UnassociatedTags { get; set; } = [];
    }
}
