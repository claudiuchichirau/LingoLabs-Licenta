using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandHandler: IRequestHandler<CreateEnrollmentCommand, CreateEnrollmentCommandResponse>
    {
        private readonly IEnrollmentRepository repository;

        public CreateEnrollmentCommandHandler(IEnrollmentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateEnrollmentCommandResponse> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEnrollmentCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                return new CreateEnrollmentCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var enrollment = Enrollment.Create(request.UserId, request.LanguageId);
            if(enrollment.IsSuccess)
            {
                await repository.AddAsync(enrollment.Value);
                return new CreateEnrollmentCommandResponse()
                {
                    Enrollment = new CreateEnrollmentDto
                    {
                        EnrollmentId = enrollment.Value.EnrollmentId,
                        UserId = enrollment.Value.UserId,
                        LanguageId = enrollment.Value.LanguageId
                    }
                };
            }

            return new CreateEnrollmentCommandResponse()
            {
                Success = false,
                ValidationsErrors = new List<string> { enrollment.Error }
            };
        }
    }
}
