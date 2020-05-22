using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Core.Quiz.Questions;
using Xunit;

namespace Azmoon.Tests.Questions.Policies
{
    public class TrueFalseQuestionPolicyTests
    {
        private readonly TrueFalseQuestionPolicy policy;
        private readonly Question question;

        public TrueFalseQuestionPolicyTests()
        {
            question = new Question()
            {
                QuestionType = QuestionType.TrueFalse,
                Title = "True/False Question"
            };
            policy = new TrueFalseQuestionPolicy(question);
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
             new TrueFalseQuestionPolicy(question)
            );
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_OnlyOneChoice_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Correct", true);
            string message = "True/False question must have 2 choices!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_MoreThanTwoChoices_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Correct", true);
            question.AddChoice("Incorrect", false);
            question.AddChoice("thirdChoice", false);
            string message = "True/False question must have 2 choices!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_HasNoCorrectChoice_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Incorrect1", false);
            question.AddChoice("Incorrect2", false);
            string message = "True/False question must have 1 correct choice!";
            
            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_MoreThanOneCorrectChoices_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Correct1", true);
            question.AddChoice("Correct2", true);
            string message = "True/False question must have 1 correct choice!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_TwoChoicesOneCorrectChoice_Should_Not_RaiseError()
        {
            // Arrange
            question.AddChoice("Correct", true);
            question.AddChoice("Incorrect", false);

            // Act

            // Assert
            policy.CheckPolicies();
        }
    }
}
