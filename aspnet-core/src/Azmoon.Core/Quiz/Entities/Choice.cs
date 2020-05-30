﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Azmoon.Core.Quiz.Interfaces;
using System;

namespace Azmoon.Core.Quiz.Entities
{
    public class Choice: FullAuditedEntity<Guid>, IMayHaveTenant, IMayBePublic, INeedHostApproval
    {
        public Choice(Guid questionId, string value, bool isCorrect, int? orderNo = null)
        {
            QuestionId = questionId;
            Value = value;
            IsCorrect = isCorrect;
            OrderNo = orderNo;
        }
        public int? TenantId { get; set; }

        public Guid QuestionId { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; private set; } = false;
        public int? OrderNo { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
    }
}