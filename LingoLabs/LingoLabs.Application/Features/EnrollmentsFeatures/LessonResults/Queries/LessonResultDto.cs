﻿namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries
{
    public class LessonResultDto
    {
        public Guid LessonResultId { get; set; }
        public Guid LessonId { get; set; }
        public Guid ChapterResultId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
