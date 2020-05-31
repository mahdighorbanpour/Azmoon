using Abp.Domain.Repositories;
using Azmoon.Admin.Application.Quiz.Categories;
using Azmoon.Application.Shared.Quiz.Questions.Dto;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azmoon.Tests.Questions
{
    public abstract class QuestionTestsBase: AzmoonTestBase
    {
        public IRepository<Category> CategoryRepo { get; }
        public IAdminCategoryAppService AdminCategoryAppService { get; }

        public QuestionTestsBase()
        {
            CategoryRepo = Resolve<IRepository<Category>>();
            AdminCategoryAppService = Resolve<IAdminCategoryAppService>();
        }

        protected void InsertTestCategories()
        {
            CategoryRepo.InsertAsync(new Category()
            {
                Title = "Category A",
                ShortDescription = "Short description for category A",
                LongDescription = "Long description for category A",
            });

            var publicCategory = CategoryRepo.InsertAsync(new Category()
            {
                Title = "Category B",
                ShortDescription = "Short description for category B",
                LongDescription = "Long description for category B",
                IsPublic = true,
            });
            LoginAsHostAdmin();
            AdminCategoryAppService.ApproveIsPublic(publicCategory.Result.Id);
            LoginAsDefaultTenantAdmin();
        }

        protected CreateUpdateQuestionDto GetCreateUpdateQuestionDtoA()
        {
            var questionA = new CreateUpdateQuestionDto()
            {
                CategoryId = CategoryRepo.FirstOrDefault(c => true).Id,
                Description = "Question A Description",
                Title = "Question A Title",
                QuestionType = QuestionType.TrueFalse,
                Hint = "Question A hints",
                Marks = 3,
                RandomizeChoices = false
            };
            questionA.Choices = new List<CreateUpdateChoiceDto>();
            questionA.Choices.Add( new CreateUpdateChoiceDto()
            {
                Value = "Choice 1",
                IsCorrect = true
            });
            questionA.Choices.Add(new CreateUpdateChoiceDto()
            {
                Value = "Choice 2",
                IsCorrect = false
            });

            return questionA;
        }
    }
}
