using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, CreateQuizCommandResponse>
    {
        private readonly ILessonRepository lessonRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IChoiceRepository choiceRepository;

        public CreateQuizCommandHandler(ILessonRepository lessonRepository, IQuestionRepository questionRepository, IChoiceRepository choiceRepository)
        {
            this.lessonRepository = lessonRepository;
            this.questionRepository = questionRepository;
            this.choiceRepository = choiceRepository;
        }
        public async Task<CreateQuizCommandResponse> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuizCommandValidator();
            var validtionResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validtionResult.IsValid)
            {
                return new CreateQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validtionResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            List<QuestionCreatedDto> questions = [];

            foreach (var q in request.QuestionList)
            {
                var question = Question.Create(q.QuestionRequirement, q.QuestionLearningType, request.LessonId);

                if(!question.IsSuccess)
                {
                    return new CreateQuizCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = [question.Error]
                    };
                }

                var result = await questionRepository.AddAsync(question.Value);

                if(!result.IsSuccess)
                {
                    return new CreateQuizCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = [result.Error]
                    };
                }

                QuestionCreatedDto questionCreatedDto = new QuestionCreatedDto
                {
                    QuestionId = question.Value.QuestionId,
                };

                foreach (var c in q.Choices)
                {
                    var choice = Choice.Create(c.ChoiceContent, c.IsCorrect, question.Value.QuestionId);

                    if (!choice.IsSuccess)
                    {
                        return new CreateQuizCommandResponse
                        {
                            Success = false,
                            ValidationsErrors = [choice.Error]
                        };
                    }

                    var choiceResult = await choiceRepository.AddAsync(choice.Value);

                    if (!choiceResult.IsSuccess)
                    {
                        return new CreateQuizCommandResponse
                        {
                            Success = false,
                            ValidationsErrors = [choiceResult.Error]
                        };
                    }

                    ChoiceCreatedDto choiceCreatedDto = new ChoiceCreatedDto()
                    {
                        ChoiceId = choice.Value.ChoiceId,
                        Choice = c
                    };

                    questionCreatedDto.Choices.Add(choiceCreatedDto);
                }
                questions.Add(questionCreatedDto);
            }

            return new CreateQuizCommandResponse
            {
                Success = true,
                Quiz = new QuizDto { Questions = questions }
            };
        }
    }
}
