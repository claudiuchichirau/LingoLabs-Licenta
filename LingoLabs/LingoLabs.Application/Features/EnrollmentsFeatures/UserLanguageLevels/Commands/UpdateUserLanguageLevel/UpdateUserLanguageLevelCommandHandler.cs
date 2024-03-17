using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.UpdateUserLanguageLevel
{
    public class UpdateUserLanguageLevelCommandHandler : IRequestHandler<UpdateUserLanguageLevelCommand, UpdateUserLanguageLevelCommandResponse>
    {
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;

        public UpdateUserLanguageLevelCommandHandler(IUserLanguageLevelRepository userLanguageLevelRepository)
        {
            this.userLanguageLevelRepository = userLanguageLevelRepository;
        }
        public async Task<UpdateUserLanguageLevelCommandResponse> Handle(UpdateUserLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserLanguageLevelCommandValidator();
            var validationResult = await validator.ValidateAsync( request, cancellationToken );

            if(!validationResult.IsValid)
            {
                return new UpdateUserLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userLanguageLevel = await userLanguageLevelRepository.FindByIdAsync(request.UserLanguageLevelId);

            if(!userLanguageLevel.IsSuccess)
            {
                return new UpdateUserLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { userLanguageLevel.Error }
                };
            }

            var updateUserLanguageLevel = request.UpdateUserLanguageLevel;

            userLanguageLevel.Value.Update(updateUserLanguageLevel.LanguageLevelId);

            await userLanguageLevelRepository.UpdateAsync(userLanguageLevel.Value);

            return new UpdateUserLanguageLevelCommandResponse
            {
                Success = true,
                UserLanguageLevel = new UpdateUserLanguageLevelDto
                {
                    LanguageLevelId = userLanguageLevel.Value.LanguageLevelId
                }
            };
        }
    }
}
