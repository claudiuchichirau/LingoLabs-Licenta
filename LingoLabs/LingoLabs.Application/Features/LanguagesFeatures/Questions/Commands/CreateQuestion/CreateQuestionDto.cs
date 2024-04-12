﻿using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.CreateQuestion
{
    public class CreateQuestionDto
    {
        public Guid QuestionId { get; set; }
        public string? QuestionRequirement { get; set; }
        public QuestionType? QuestionType { get; set; }
        public Guid? LessonId { get; set; }
    }
}
