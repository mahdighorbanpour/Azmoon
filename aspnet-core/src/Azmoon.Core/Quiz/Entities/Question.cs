﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Core.Quiz.Exceptions;
using Azmoon.Core.Quiz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Azmoon.Core.Quiz.Entities
{
    public class Question: FullAuditedEntity<Guid>, IMayHaveTenant, IMayBePublic, INeedHostApproval
    {
        public Question()
        {
            
        }
        public int? TenantId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        public int Marks { get; set; }
        public QuestionType QuestionType { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<QuizQuestion> Quizzes { get; set; }

        private readonly List<Choice> _choices = new List<Choice>();
        public IReadOnlyList<Choice> Choices => _choices.AsReadOnly();
        public bool? RandomizeChoices { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public void AddChoice(string value, bool isCorrect)
        {
            if (_choices.Any(c => c.Value == value))
                throw new InvalidChoiceException($"There is alreay a choice with value of {value}");

            _choices.Add(new Choice(Id, value, isCorrect));
        }

        public int ChoicesCount => Choices.Count;

        public int CorrectChoicesCount => Choices.Count(c => c.IsCorrect);

    }
}
