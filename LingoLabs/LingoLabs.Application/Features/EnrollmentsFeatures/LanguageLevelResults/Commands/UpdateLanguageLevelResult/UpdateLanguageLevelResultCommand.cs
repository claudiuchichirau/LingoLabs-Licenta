﻿using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.UpdateLanguageLevelResult
{
    public class UpdateLanguageLevelResultCommand: IRequest<UpdateLanguageLevelResultCommandResponse>
    {
        public Guid LanguageLevelResultId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
