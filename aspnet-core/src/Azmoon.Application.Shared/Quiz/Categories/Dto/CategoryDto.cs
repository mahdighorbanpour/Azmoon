using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Application.Shared.Quiz.Categories.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class CategoryDto : EntityDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUri { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public int QuestionsCount { get; set; }
        public int QuizzesCount { get; set; }
    }
}
