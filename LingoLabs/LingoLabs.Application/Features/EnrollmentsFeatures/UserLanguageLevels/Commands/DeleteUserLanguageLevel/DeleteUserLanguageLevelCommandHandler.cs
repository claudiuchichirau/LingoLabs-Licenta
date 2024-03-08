using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel
{
    public class DeleteUserLanguageLevelCommandHandler : IRequestHandler<DeleteUserLanguageLevelCommand, DeleteUserLanguageLevelCommandResponse>
    {
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;

        public DeleteUserLanguageLevelCommandHandler(IUserLanguageLevelRepository userLanguageLevelRepository)
        {
            this.userLanguageLevelRepository = userLanguageLevelRepository;
        }
        public async Task<DeleteUserLanguageLevelCommandResponse> Handle(DeleteUserLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteUserLanguageLevelCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteUserLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var userLanguageLevel = await userLanguageLevelRepository.FindByIdAsync(request.UserLanguageLevelId);

            if(!userLanguageLevel.IsSuccess)
            {
                return new DeleteUserLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { userLanguageLevel.Error }
                };
            }

            await userLanguageLevelRepository.DeleteAsync(request.UserLanguageLevelId);

            return new DeleteUserLanguageLevelCommandResponse
            {
                Success = true
            };
        }
    }
}
