using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Azmoon.Application.Shared.Quiz.Quiz.Dto
{
    [AutoMapFrom(typeof(Core.Quiz.Entities.Quiz))]
    public class QuizDto : EntityDto<Guid>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public TimeSpan? Duration { get; set; }
        public bool IsActive { get; set; }
        public int TotalQuestion { get; set; }
        public int TotalMarks { get; set; }
    }
}
