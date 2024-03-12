using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageLevelId
{
    public class GetAllQuestionsByLanguageLevelIdQuery: IRequest<GetAllQuestionsByLanguageLevelIdResponse>
    {
        public Guid LanguageLevelId { get; set; }
    }
}
