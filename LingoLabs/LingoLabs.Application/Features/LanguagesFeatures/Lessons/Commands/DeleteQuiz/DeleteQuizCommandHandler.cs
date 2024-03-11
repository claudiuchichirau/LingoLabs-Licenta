using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteQuiz
{
    public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, DeleteQuizCommandResponse>
    {
        private readonly ILessonRepository lessonRepository;
        private readonly IQuestionRepository questionRepository;

        public DeleteQuizCommandHandler(ILessonRepository lessonRepository, IQuestionRepository questionRepository)
        {
            this.lessonRepository = lessonRepository;
            this.questionRepository = questionRepository;
        }
        public async Task<DeleteQuizCommandResponse> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteQuizCommandValidator();
            var validationResponse = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResponse.IsValid) 
            {
                return new DeleteQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResponse.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var lesson = await lessonRepository.FindByIdAsync(request.LessonId);

            if(!lesson.IsSuccess)
            {
                return new DeleteQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { lesson.Error }
                };
            }

            List<Guid> questionsIds = new List<Guid>();
            foreach (var questionId in lesson.Value.LessonQuestions)
            {
                questionsIds.Add(questionId.QuestionId);
            }

            foreach (var questionId in questionsIds)
            {
                await questionRepository.DeleteAsync(questionId);
            }

            return new DeleteQuizCommandResponse
            {
                Success = true
            };
        }
    }
}
