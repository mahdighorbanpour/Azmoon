using Abp.Application.Services.Dto;

namespace Azmoon.Application.Shared.Quiz.Quiz.Dto
{
    public class PagedQuizResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
        public bool? IsActive { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsPublic { get; set; }
    }
}
