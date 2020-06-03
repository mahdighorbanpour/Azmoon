using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Admin.Application.Questions;
using Xunit;

namespace Azmoon.Tests.Questions.Policies
{
    public class FillInTheBlankQuestionPolicyTests
    {
        private readonly FillInTheBlankQuestionPolicy policy;
        private readonly Question question;

        public FillInTheBlankQuestionPolicyTests()
        {
            question = new Question()
            {
                QuestionType = QuestionType.FillInTheBlank,
                Title = "True/False Question"
            };
            policy = new FillInTheBlankQuestionPolicy(question);
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
        public void CheckPolicies_NoChoice_Should_RaiseError()
        {
            // Arrange
            string message = "FillInTheBlank question must have at least 1 choice with 1 blank!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_OneChoiceNoBlanks_Should_RaiseError()
        {
            // Arrange
            var choice1 = question.AddChoice("Choice 1", false);
            string message = "FillInTheBlank question must have at least 1 choice with 1 blank!";
            
            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_ChoicesWithNoBlanks_Should_RaiseError()
        {
            // Arrange
            var choice1 = question.AddChoice("Choice 1", false);
            choice1.AddBlank(1, "answer");
            var choice2 = question.AddChoice("Choice 2", false);
            string message = "FillInTheBlank question all choices must have at least 1 blank!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_ChoicesWithRepeatedBlankIndexes_Should_RaiseError()
        {
            // Arrange
            var choice1 = question.AddChoice("Choice 1", false);
            choice1.AddBlank(1, "a");
            choice1.AddBlank(1, "b");
            string message = "FillInTheBlank a choice cannot have repeated blank indexes!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_ChoicesWithBlankIndexesGreaterThanValueLength_Should_RaiseError()
        {
            // Arrange
            var choice1 = question.AddChoice("Choice 1", false);
            choice1.AddBlank(1, "a");
            choice1.AddBlank(100, "b");
            string message = "FillInTheBlank a choice cannot have a blank index greater than it's lenght!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_ChoicesWithAtLeastOneCorrectIndex_Should_Not_RaiseError()
        {
            // Arrange
            var choice1 = question.AddChoice("Choice 1", false);
            choice1.AddBlank(1, "a");

            // Act

            // Assert
            policy.CheckPolicies();
        }
    }
}
