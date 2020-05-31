using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Azmoon.Admin.Application.Quiz.Questions;
using Azmoon.Application.Shared.Quiz.Questions.Dto;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace Azmoon.Tests.Questions
{
    public class AdminQuestionAppServiceTests: QuestionTestsBase
    {
        private readonly IAdminQuestionAppService adminQuestionAppService;


        public AdminQuestionAppServiceTests()
        {
            adminQuestionAppService = Resolve<IAdminQuestionAppService>();
            InsertTestCategories();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnMatchDto()
        {
            // Arrange
            var questionDto = GetCreateUpdateQuestionDtoA();

            // Act
            var createdQuestion = await adminQuestionAppService.CreateAsync(questionDto);

            // Assert
            createdQuestion.CategoryId.ShouldBe(CategoryRepo.FirstOrDefault(c => true).Id);
            createdQuestion.Description.ShouldBe("Question A Description");
            createdQuestion.Title.ShouldBe("Question A Title");
            createdQuestion.QuestionType.ShouldBe(QuestionType.TrueFalse);
            createdQuestion.Hint.ShouldBe("Question A hints");
            createdQuestion.Marks.ShouldBe(3);
            createdQuestion.RandomizeChoices.ShouldBe(false);

            createdQuestion.ChoicesCount.ShouldBe(2);
            createdQuestion.Choices[0].Value.ShouldBe("Choice 1");
            createdQuestion.Choices[0].IsCorrect.ShouldBe(true);
            createdQuestion.Choices[1].Value.ShouldBe("Choice 2");
            createdQuestion.Choices[1].IsCorrect.ShouldBe(false);
        }

        [Fact]
        public async Task UpdateAsync_SameChoices_ShouldNotChangeChoices()
        {
            // Arrange
            var questionDto = GetCreateUpdateQuestionDtoA();
            var createdQuestion = await adminQuestionAppService.CreateAsync(questionDto);

            questionDto.Id = createdQuestion.Id;
            questionDto.Choices[0].Id = createdQuestion.Choices[0].Id;
            questionDto.Choices[0].Value = "New Choice 1";
            questionDto.Choices[0].IsCorrect = false;

            questionDto.Choices[1].Id = createdQuestion.Choices[1].Id;
            questionDto.Choices[1].Value = "New Choice 2";
            questionDto.Choices[1].IsCorrect = true;
            questionDto.IsPublic = true;

            // Act
            var updatedQuestion = await adminQuestionAppService.UpdateAsync(questionDto);

            // Assert
            updatedQuestion.ChoicesCount.ShouldBe(2);
            updatedQuestion.Choices[0].Id.ShouldBe(createdQuestion.Choices[0].Id);
            updatedQuestion.Choices[1].Id.ShouldBe(createdQuestion.Choices[1].Id);
            updatedQuestion.Choices[0].Value.ShouldBe("New Choice 1");
            updatedQuestion.Choices[0].IsCorrect.ShouldBeFalse();
            updatedQuestion.Choices[0].IsPublic.ShouldBeTrue();
            updatedQuestion.Choices[0].IsApproved.ShouldBeNull();
            updatedQuestion.Choices[1].Value.ShouldBe("New Choice 2");
            updatedQuestion.Choices[1].IsCorrect.ShouldBeTrue();
            updatedQuestion.Choices[1].IsPublic.ShouldBeTrue();
            updatedQuestion.Choices[1].IsApproved.ShouldBeNull();
            
        }

        [Fact]
        public async Task UpdateAsync_SomeNewChoices_ShouldDeleteAndAddChangedChoices()
        {
            // Arrange
            var questionDto = GetCreateUpdateQuestionDtoA();
            var createdQuestion = await adminQuestionAppService.CreateAsync(questionDto);

            questionDto.Id = createdQuestion.Id;
            questionDto.Choices[0].Id = createdQuestion.Choices[0].Id;
            questionDto.Choices[0].Value = "New Choice 1";
            questionDto.Choices[0].IsCorrect = false;

            questionDto.Choices.RemoveAt(1);
            questionDto.Choices.Add( new CreateUpdateChoiceDto()
            {
                IsCorrect = true,
                Value = "New Choice 2"
            });

            // Act
            var updatedQuestion = await adminQuestionAppService.UpdateAsync(questionDto);

            // Assert
            updatedQuestion.ChoicesCount.ShouldBe(2);
            updatedQuestion.Choices[0].Id.ShouldBe(createdQuestion.Choices[0].Id);
            updatedQuestion.Choices[1].Id.ShouldNotBe(createdQuestion.Choices[1].Id);

            updatedQuestion.Choices[0].Value.ShouldBe("New Choice 1");
            updatedQuestion.Choices[0].IsCorrect.ShouldBeFalse();
            updatedQuestion.Choices[1].Value.ShouldBe("New Choice 2");
            updatedQuestion.Choices[1].IsCorrect.ShouldBeTrue();

        }

        [Fact]
        public async Task ApproveIsPublic_ShouldAlsoApproveChoices()
        {
            // Arrange

            var questionDto = GetCreateUpdateQuestionDtoA();
                questionDto.CategoryId = CategoryRepo.FirstOrDefault(c => c.IsPublic && c.IsApproved.HasValue && c.IsApproved.Value).Id;
            questionDto.IsPublic = true;
            var createdQuestion = await adminQuestionAppService.CreateAsync(questionDto);
            var dbContext = Resolve<AzmoonDbContext>();
            // Act
            LoginAsHostAdmin();
            await adminQuestionAppService.ApproveIsPublic(createdQuestion.Id);

            var approvedQuestion = dbContext.Questions.Include(q => q.Choices).SingleOrDefault(q => q.Id == createdQuestion.Id);
            // Assert
            approvedQuestion.Choices.Count.ShouldBe(2);

            approvedQuestion.Choices[0].IsPublic.ShouldBeTrue();
            approvedQuestion.Choices[0].IsApproved.ShouldNotBeNull();
            approvedQuestion.Choices[0].IsApproved.ShouldBe(true);

            approvedQuestion.Choices[1].IsPublic.ShouldBeTrue();
            approvedQuestion.Choices[1].IsApproved.ShouldNotBeNull();
            approvedQuestion.Choices[1].IsApproved.ShouldBe(true);
        }

    }
}
