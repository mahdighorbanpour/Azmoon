using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Admin.Application.Questions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Azmoon.Tests.Questions
{
    public class QuestionManagerTests : AzmoonTestBase
    {
        private readonly IQuestionManager questionManager;
        private readonly Question question;

        public QuestionManagerTests()
        {
            questionManager = Resolve<IQuestionManager>();
            question = new Question();
        }

        [Fact]
        public async Task CreateAsync_TrueFalse_OnlyOneChoice_Should_RaiseError()
        {
            // Arrange
            question.QuestionType = QuestionType.TrueFalse;
            question.AddChoice("Correct", true);
            string message = "True/False question must have 2 choices!";
            
            // Act

            // Assert
           var exception = await Assert.ThrowsAsync<UserFriendlyException>(
               async () => await questionManager.CreateAsync(question)
               );
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task CreateAsync_TrueFalse_ValidChoices_Should_Save()
        {
            // Arrange
            question.QuestionType = QuestionType.TrueFalse;
            question.Title = "True/False Question";
            question.AddChoice("Correct", true);
            question.AddChoice("Inorrect", false);

            // Act
            var savedQuestion = await questionManager.CreateAsync(question);

            // Assert
            savedQuestion.Id.ShouldNotBeNull();
            savedQuestion.Title.ShouldBe("True/False Question");
            savedQuestion.AllChoicesCount.ShouldBe(2);
            savedQuestion.CorrectChoicesCount.ShouldBe(1);
        }
    }
}
