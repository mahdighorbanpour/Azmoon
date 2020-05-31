using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Admin.Application.Questions;
using Shouldly;
using Xunit;

namespace Azmoon.Tests.Questions.Policies
{
    public class QuestionPolicyFactoryTests
    {
        private readonly Question question;
        private readonly IQuestionPolicyFactory questionPolicyFactory;

        public QuestionPolicyFactoryTests()
        {
            question = new Question()
            {
                Title = "Question"
            };
            questionPolicyFactory = new QuestionPolicyFactory();
        }

        [Fact]
        public void CreatePolicy_TrueFalseQuestion_Should_Return_Match_Policy()
        {
            // Arrange
            question.QuestionType = QuestionType.TrueFalse;
            // Act
            var policy = questionPolicyFactory.CreatePolicy(question);
            // Assert
            policy.ShouldBeOfType<TrueFalseQuestionPolicy>();
        }
    }
}
