using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;

namespace LingoLabs.App.Contracts.LanguageContracts
{
    public interface IListeningLessonDataService : ILessonDataService
    {
        Task<ApiResponse<ListeningLessonViewModel>> CreateListeningLessonAsync (ListeningLessonViewModel createListeningLessonViewModel);
        Task<ApiResponse<ListeningLessonViewModel>> UpdateListeningLessonAsync (ListeningLessonViewModel updateListeningLessonViewModel);
    }
}
