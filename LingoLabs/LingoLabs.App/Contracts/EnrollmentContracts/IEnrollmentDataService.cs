using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface IEnrollmentDataService
    {
        Task<ApiResponse<EnrollmentViewModel>> CreateEnrollmentAsync(EnrollmentViewModel createEnrollmentViewModel);
        Task<ApiResponse<EnrollmentViewModel>> DeleteEnrollmentAsync(Guid enrollmentId);
        Task<List<EnrollmentViewModel>> GetAllEnrollmentsAsync();
        Task<List<EnrollmentViewModel>> GetAllEnrollmentsByUserIdAsync();
        Task<EnrollmentViewModel> GetEnrollmentByIdAsync(Guid enrollmentId);
    }
}
