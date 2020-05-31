using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    [AutoMapTo(typeof(Choice))]
    public class CreateUpdateChoiceDto: EntityDto<Guid>
    {
        public Guid QuestionId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public int? OrderNo { get; set; }
    }


}
