using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Azmoon.Core.Quiz.Entities
{
    public class Category: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUri { get; set; }

        public List<Question> Questions { get; set; }

        public int TotalQuestion { get { return Questions.Count; } }

    }
}
