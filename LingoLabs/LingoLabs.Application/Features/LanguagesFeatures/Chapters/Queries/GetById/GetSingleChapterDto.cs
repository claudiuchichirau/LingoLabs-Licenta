﻿using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries.GetById
{
    public class GetSingleChapterDto: ChapterDto
    {
        public string ChapterDescription { get; set; } = string.Empty;
        public int? ChapterPriorityNumber { get; set; } 
        public byte[] ChapterImageData { get; set; } = [];
        public string ChapterVideoLink { get; set; } = string.Empty;
        public List<LanguageCompetenceDto> languageCompetences { get; set; } = [];
        public List<LessonDto> ChapterLessons { get; set; } = [];
        public List<TagDto> ChapterKeyWords { get; set; } = [];
        public List<TagDto> UnassociatedTags { get; set; } = [];
    }
}
