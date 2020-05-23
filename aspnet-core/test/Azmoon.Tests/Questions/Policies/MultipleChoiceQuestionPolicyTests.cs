using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Core.Quiz.Questions;
using Xunit;

namespace Azmoon.Tests.Questions.Policies
{
    public class MultipleChoiceQuestionPolicyTests
    {
        private readonly MultipleChoiceQuestionPolicy policy;
        private readonly Question question;

        public MultipleChoiceQuestionPolicyTests()
        {
            question = new Question()
            {
                QuestionType = QuestionType.MultipleChoice,
                Title = "Multiple choice Question"
            };
            policy = new MultipleChoiceQuestionPolicy(question);
        }

        [Fact]
        public void CheckPolicies_IncompatiblePolicy_Should_RaiseError()
        {
            // Arrange
            question.QuestionType = QuestionType.ShortAnswer;
            string message = "Incompatible policy cheker is selected!";
            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() =>
             new MultipleChoiceQuestionPolicy(question)
            );
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_OnlyOneChoice_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", true);
            string message = "Multiple choice question must have 2 choices or more!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_HasNoCorrectChoice_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", false);
            question.AddChoice("Choice 2", false);
            string message = "Question must have at least 1 correct choice!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_MorThanOneChoicesOneCorrectChoice_Should_Not_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", true);
            question.AddChoice("Choice 2", false);

            // Act

            // Assert
            policy.CheckPolicies();
        }
    }
}
