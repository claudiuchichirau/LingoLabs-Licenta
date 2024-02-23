using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Queries.GetById
{
    public class GetByIdUserLanguageLevelQueryHandler : IRequestHandler<GetByIdUserLanguageLevelQuery, UserLanguageLevelDto>
    {
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;

        public GetByIdUserLanguageLevelQueryHandler(IUserLanguageLevelRepository userLanguageLevelRepository)
        {
            this.userLanguageLevelRepository = userLanguageLevelRepository;
        }
        public async Task<UserLanguageLevelDto> Handle(GetByIdUserLanguageLevelQuery request, CancellationToken cancellationToken)
        {
            var userLanguageLevel = await userLanguageLevelRepository.FindByIdAsync(request.Id);
            if(userLanguageLevel.IsSuccess)
            {
                return new UserLanguageLevelDto
                {
                    UserLanguageLevelId = userLanguageLevel.Value.UserLanguageLevelId,
                    EnrollmentId = userLanguageLevel.Value.EnrollmentId,
                    LanguageCompetenceId = userLanguageLevel.Value.LanguageCompetenceId,
                    LanguageLevelId = userLanguageLevel.Value.LanguageLevelId
                };
            }

            return new UserLanguageLevelDto();
        }
    }
}
