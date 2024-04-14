using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;

namespace LingoLabs.App.Contracts.LanguageContracts
{
    public interface IChoiceDataService
    {
        Task<ChoiceViewModel> GetChoiceByIdAsync(Guid choiceId);
        Task<ApiResponse<ChoiceViewModel>> CreateChoiceAsync(ChoiceViewModel createChoiceViewModel);
        Task<ApiResponse<ChoiceViewModel>> UpdateChoiceAsync(ChoiceViewModel createChoiceViewModel);
        Task<ApiResponse<ChoiceViewModel>> DeleteChoiceAsync(Guid choiceId);
    }
}
