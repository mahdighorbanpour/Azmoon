using Abp.Application.Services.Dto;

namespace Azmoon.Application.Shared.Quiz.Quiz.Dto
{
    public class PagedQuizResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
        public bool? IsActive { get; set; }
    }
}
