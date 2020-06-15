using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Admin.Application.Questions;
using Xunit;

namespace Azmoon.Tests.Questions.Policies
{
    public class ShortAnswerQuestionPolicyTests
    {
        private readonly ShortAnswerQuestionPolicy policy;
        private readonly Question question;

        public ShortAnswerQuestionPolicyTests()
        {
            question = new Question()
            {
                QuestionType = QuestionType.ShortAnswer,
                Title = "True/False Question"
            };
            policy = new ShortAnswerQuestionPolicy(question);
        }

        [Fact]
        public void CheckPolicies_IncompatiblePolicy_Should_RaiseError()
        {
            // Arrange
            question.QuestionType = QuestionType.TrueFalse;
            string message = "Incompatible policy cheker is selected!";
            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() =>
             new ShortAnswerQuestionPolicy(question)
            );
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_MoreThanOneChoice_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", true);
            question.AddChoice("Choice 2", true);
            string message = "Short answer question must have only 1 choice which should be correct!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_OneNotCorrectChoice_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", false);
            string message = "Short answer question must have only 1 choice which should be correct!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_OneCorrectChoice_Should_Not_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", true);

            // Act

            // Assert
            policy.CheckPolicies();
        }
    }
}
