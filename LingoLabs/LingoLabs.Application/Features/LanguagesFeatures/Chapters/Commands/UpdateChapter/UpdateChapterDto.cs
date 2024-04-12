﻿namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterDto
    {
        public string ChapterName { get; set; } = string.Empty;
        public string ChapterDescription { get; set; } = string.Empty;
        public int? ChapterPriorityNumber { get; set; }
        public string ChapterImageData { get; set; } = string.Empty;
        public string ChapterVideoLink { get; set; } = string.Empty;
    }
}
