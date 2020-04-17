using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Application.Quiz.Categories.Dto
{
    [AutoMapTo(typeof(Category))]
    public class CreateCategoryDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUri { get; set; }
    }
}
