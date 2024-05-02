using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetById
{
    public class GetByIdEnrollmentQueryHandler : IRequestHandler<GetByIdEnrollmentQuery, GetSingleEnrollmentDto>
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetByIdEnrollmentQueryHandler(IEnrollmentRepository enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }
        public async Task<GetSingleEnrollmentDto> Handle(GetByIdEnrollmentQuery request, CancellationToken cancellationToken)
        {
            var enrollment = await enrollmentRepository.FindByIdAsync(request.Id);
            if(enrollment.IsSuccess)
            {
                return new GetSingleEnrollmentDto
                {
                    EnrollmentId = enrollment.Value.EnrollmentId,
                    UserId = enrollment.Value.UserId,
                    LanguageId = enrollment.Value.LanguageId,

                    LanguageLevelResults = enrollment.Value.LanguageLevelResults.Select(LanguageLevelResults => new LanguageLevelResults.Queries.LanguageLevelResultDto
                    {
                        EnrollmentId = LanguageLevelResults.EnrollmentId,
                        LanguageLevelId = LanguageLevelResults.LanguageLevelId,
                        LanguageLevelResultId = LanguageLevelResults.LanguageLevelResultId,
                        IsCompleted = LanguageLevelResults.IsCompleted
                    }).ToList(),

                    LanguageCompetenceResults = enrollment.Value.LanguageCompetenceResults.Select(LanguageCompetenceResults => new LanguageCompetenceResults.Queries.LanguageCompetenceResultDto
                    {
                        EnrollmentId = LanguageCompetenceResults.EnrollmentId,
                        LanguageCompetenceId = LanguageCompetenceResults.LanguageCompetenceId,
                        LanguageCompetenceResultId = LanguageCompetenceResults.LanguageCompetenceResultId,
                        IsCompleted = LanguageCompetenceResults.IsCompleted
                    }).ToList(),

                    UserLanguageLevels = enrollment.Value.UserLanguageLevels.Select(UserLanguageLevels => new UserLanguageLevels.Queries.UserLanguageLevelDto
                    {
                        UserLanguageLevelId = UserLanguageLevels.UserLanguageLevelId,
                        EnrollmentId = UserLanguageLevels.EnrollmentId,
                        LanguageCompetenceId = UserLanguageLevels.LanguageCompetenceId,
                        LanguageLevelId = UserLanguageLevels.LanguageLevelId
                    }).ToList()

                };
            }

            return new GetSingleEnrollmentDto();
        }
    }
}
