using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Azmoon.Core.Quiz.Interfaces;
using System;
using System.Collections.Generic;

namespace Azmoon.Core.Quiz.Entities
{
    public class Choice : FullAuditedEntity<Guid>, IMayHaveTenant, IMayBePublic, INeedHostApproval
    {
        public Choice(Guid questionId, string value, bool isCorrect = false, int? orderNo = null)
        {
            QuestionId = questionId;
            Value = value;
            IsCorrect = isCorrect;
            OrderNo = orderNo;
            Blanks = new List<Blank>();
        }

        public Choice(Guid questionId, string value, bool isCorrect = false, int? orderNo = null, MatchSet matchSet = null)
            : this(questionId, value, isCorrect, orderNo)
        {
            MatchSet = matchSet;
        }

        public int? TenantId { get; set; }

        public Guid QuestionId { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; private set; } = false;
        public int? OrderNo { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public Guid? MatchSetId { get; set; }
        public MatchSet MatchSet { get; set; }
        public List<Blank> Blanks { get; private set; }
        public void SetIsCorrect(bool value)
        {
            IsCorrect = value;
        }

        public void AddBlank(int index, string answer)
        {
            Blank blank = new Blank()
            {
                Index = index,
                Answer = answer,
                ChoiceId = Id,
                Choice = this,
                IsPublic = IsPublic
            };
            Blanks.Add(blank);
        }
    }
}