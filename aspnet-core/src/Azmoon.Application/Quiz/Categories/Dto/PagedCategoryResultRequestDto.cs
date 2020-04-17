using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azmoon.Application.Quiz.Categories.Dto
{
    public class PagedCategoryResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
