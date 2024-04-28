using LingoLabs.App.ViewModel.EnrollmentModels;
using LingoLabs.App.ViewModel.LanguageModels.EnrollmentModels;

namespace LingoLabs.App.ViewModel.Responses
{
    public class UserEnrollmentsResponse
    {
        public List<EnrollmentViewModel> Enrollments { get; set; } = [];
    }
}
