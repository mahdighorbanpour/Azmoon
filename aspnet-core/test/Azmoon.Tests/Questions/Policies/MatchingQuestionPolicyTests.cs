using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Admin.Application.Questions;
using Xunit;
using System;

namespace Azmoon.Tests.Questions.Policies
{
    public class MatchingQuestionPolicyTests
    {
        private readonly MatchingQuestionPolicy policy;
        private readonly Question question;

        public MatchingQuestionPolicyTests()
        {
            question = new Question()
            {
                QuestionType = QuestionType.Matching,
                Title = "Ordering Question"
            };
            policy = new MatchingQuestionPolicy(question);
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
        public void CheckPolicies_LessThanTwoChoice_Should_RaiseError()
        {
            // Arrange
            question.AddChoice("Choice 1", true, 0);
            string message = "Matching question must have 2 match sets or more!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_LessThanTwoMatchSets_Should_RaiseError()
        {
            // Arrange
            var set1 = question.AddMatchSet("Set 1");
            question.AddChoice("Choice 1", false, matchSet: set1.Value);
            string message = "Matching question must have 2 match sets or more!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_HasChoicesWithNoMatchSet_Should_RaiseError()
        {
            // Arrange
            var set1 = question.AddMatchSet("Set 1");
            var set2 = question.AddMatchSet("Set 2");
            question.AddChoice("Choice 1", false, matchSet: set1.Value);
            question.AddChoice("Choice 2", false);
            string message = "Matching question all choices must have a match set!";

            // Act

            // Assert
            var exception = Assert.Throws<UserFriendlyException>(() => policy.CheckPolicies());
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_HasRepeatedMatchSet_Should_RaiseError()
        {
            // Arrange
            var set1 = question.AddMatchSet("Set 1");
            string message = "Matchset value already exists!";

            // Act

            // Assert
            var exception = Assert.Throws<Exception>(() => question.AddMatchSet("Set 1"));
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void CheckPolicies_MoreThanOneMatchSet_MoreThanOneChoiceWithMatchSet_Should_Not_RaiseError()
        {
            // Arrange
            var set1 = question.AddMatchSet("Set 1");
            var set2 = question.AddMatchSet("Set 2");
            question.AddChoice("Choice 1", false, matchSet: set1.Value);
            question.AddChoice("Choice 2", false, matchSet:set2.Value);
            // Act

            // Assert
            policy.CheckPolicies();
        }
    }
}
