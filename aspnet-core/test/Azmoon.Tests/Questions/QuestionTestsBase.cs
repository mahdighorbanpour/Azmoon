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

        protected CreateUpdateQuestionDto GetCreateUpdateQuestionDto_FillInTheBlank()
        {
            var questionDto = GetCreateUpdateQuestionDtoA();

            questionDto.QuestionType = QuestionType.FillInTheBlank;
            questionDto.Choices[0].Blanks.Add(new BlankDto() { Index = 1, Answer = "a" });
            questionDto.Choices[0].Blanks.Add(new BlankDto() { Index = 2, Answer = "b" });
            questionDto.Choices[1].Blanks.Add(new BlankDto() { Index = 3, Answer = "c" });
            questionDto.Choices[1].Blanks.Add(new BlankDto() { Index = 4, Answer = "d" });

            return questionDto;
        }
        
        protected CreateUpdateQuestionDto GetCreateUpdateQuestionDto_Matching()
        {
            var questionDto = GetCreateUpdateQuestionDtoA();

            questionDto.QuestionType = QuestionType.Matching;
            var set1 = new MatchSetDto() { Value = "Set 1" };
            var set2 = new MatchSetDto() { Value = "Set 2" };
            questionDto.MatchSets.Add(set1);
            questionDto.MatchSets.Add(set2);
            questionDto.Choices[0].MatchSet = set1;
            questionDto.Choices[1].MatchSet = set2;

            return questionDto;
        }
    }
}
