using Abp.Domain.Entities;
using Azmoon.Core.Quiz.Interfaces;
using System;

namespace Azmoon.Core.Quiz.Entities
{
    public class Blank: Entity<Guid>,IMayHaveTenant, IMayBePublic, INeedHostApproval
    {
        public int? TenantId { get; set; }
        public int Index { get; set; }
        public string Answer { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public Guid ChoiceId { get; set; }
        public Choice Choice { get; set; }
    }
}