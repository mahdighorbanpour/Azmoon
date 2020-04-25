using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Application.Shared.Quiz.Categories.Dto
{
    [AutoMapTo(typeof(Category))]
    public class CreateUpdateCategoryDto : EntityDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUri { get; set; }
        public bool IsPublic { get; set; }
    }
}
