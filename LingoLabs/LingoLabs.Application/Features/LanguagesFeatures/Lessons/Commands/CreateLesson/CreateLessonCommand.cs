﻿using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommand: IRequest<CreateLessonCommandResponse>
    {
        public string LessonTitle { get; set; } = string.Empty;
        public Guid ChapterId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
    }
}
