﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Azmoon.Core.Quiz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Azmoon.Core.Quiz.Entities
{
    public class Quiz : FullAuditedEntity<Guid>, IMayHaveTenant, IMayBePublic, INeedHostApproval
    {
        public Quiz()
        {

        }
        public int? TenantId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public List<QuizQuestion> Questions { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TotalQuestions { get { return Questions != null ? Questions.Count : 0; } }
        public int TotalMarks { get { return Questions != null ? Questions.Sum(q => q.Marks) : 0; } }
    }
}
