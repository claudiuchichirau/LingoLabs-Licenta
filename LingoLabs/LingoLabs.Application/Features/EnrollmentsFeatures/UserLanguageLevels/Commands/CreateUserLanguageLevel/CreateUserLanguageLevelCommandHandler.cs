using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.CreateUserLanguageLevel
{
    public class CreateUserLanguageLevelCommandHandler: IRequestHandler<CreateUserLanguageLevelCommand, CreateUserLanguageLevelCommandResponse>
    {
        private readonly IUserLanguageLevelRepository repository;

        public CreateUserLanguageLevelCommandHandler(IUserLanguageLevelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateUserLanguageLevelCommandResponse> Handle(CreateUserLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserLanguageLevelCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                return new CreateUserLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userLanguageLevel = UserLanguageLevel.Create(request.EnrollmentId, request.LanguageCompetenceId, request.LanguageLevelId);

            if(userLanguageLevel.IsSuccess)
            {
                await repository.AddAsync(userLanguageLevel.Value);
                return new CreateUserLanguageLevelCommandResponse
                {
                    UserLanguageLevel = new CreateUserLanguageLevelDto
                    {
                        UserLanguageLevelId = userLanguageLevel.Value.UserLanguageLevelId,
                        EnrollmentId = userLanguageLevel.Value.EnrollmentId,
                        LanguageCompetenceId = userLanguageLevel.Value.LanguageCompetenceId,
                        LanguageLevelId = userLanguageLevel.Value.LanguageLevelId
                    }
                };
            }

            return new CreateUserLanguageLevelCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { userLanguageLevel.Error }
            };
        }
    }
}
