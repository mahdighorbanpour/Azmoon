using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Core.Quiz.Questions;
using Xunit;

namespace Azmoon.Tests.Questions.Policies
{
    public class OrderingQuestionPolicyTests
    {
        private readonly OrderingQuestionPolicy policy;
        private readonly Question question;

        public OrderingQuestionPolicyTests()
        {
            question = new Question()
            {
                QuestionType = QuestionType.Ordering,
                Title = "Ordering Question"
            };
            policy = new OrderingQuestionPolicy(question);
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
             new OrderingQuestionPolicy(question)
            );
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_OnlyOneChoice_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", true, 0);
            string message = "Ordering question must have 2 choices or more!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_HasChoicesWithNoOrderNo_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", true);
            question.AddChoice("Choice 2", false);
            question.AddChoice("Choice 3", false);
            string message = "Ordering question all choices must have an order number!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_OrderNumbersAreNotInSequence_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", true, 0);
            question.AddChoice("Choice 2", false, 2);
            question.AddChoice("Choice 3", false, 1);
            string message = "Order numbers must be in a sequence starting from 0!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_RandomizeChoicesIsNotEnabled_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", false, 0);
            question.AddChoice("Choice 2", false, 1);
            string message = "Randomize choices must be checked for ordering question!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_MoreThanOneRandomizedChoiceWithCorrectOrderNumbers_Should_Not_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", false, 0);
            question.AddChoice("Choice 2", false, 1);
            question.RandomizeChoices = true;
            // Act

            // Assert
            policy.CheckPolicies();
        }
    }
}
