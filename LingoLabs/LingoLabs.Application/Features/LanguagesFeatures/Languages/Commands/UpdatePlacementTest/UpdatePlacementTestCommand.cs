﻿using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdatePlacementTest
{
    public class UpdatePlacementTestCommand: IRequest<UpdatePlacementTestCommandResponse>
    {
        public Guid LanguageId { get; set; }
        public UpdatePlacementTestDto? PlacementTest { get; set; }
    }
}
