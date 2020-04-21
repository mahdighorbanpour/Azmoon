using Abp.Application.Services.Dto;

namespace Azmoon.Application.Shared.Quiz.Categories.Dto
{
    public class PagedCategoryResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
