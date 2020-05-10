using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Azmoon.Application.Shared.Quiz.Quiz.Dto
{
    [AutoMapTo(typeof(Core.Quiz.Entities.Quiz))]
    public class CreateUpdateQuizDto : EntityDto<Guid>
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public TimeSpan? Duration { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
    }
}
