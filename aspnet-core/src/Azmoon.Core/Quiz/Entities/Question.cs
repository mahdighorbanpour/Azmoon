using Abp.Domain.Entities;
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

        private List<Choice> _choices = new List<Choice>();
        public IReadOnlyList<Choice> Choices => _choices.AsReadOnly();
        public bool? RandomizeChoices { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public Choice AddChoice(string value, bool isCorrect, int? orderNo = null)
        {
            var choice = new Choice(Id, value, isCorrect, orderNo);
            choice.IsPublic = IsPublic;
            _choices.Add(choice);
            return choice;

        }

        public Choice UpdateChoice(Choice choice)
        {
            var _choice = _choices.Find(c => c.Id == choice.Id);
            if (_choice == null)
                throw new Exception("Choice is not valid");
           
            _choice.Value = choice.Value;
            _choice.OrderNo = choice.OrderNo;
            _choice.SetIsCorrect(choice.IsCorrect);

            _choice.IsPublic = IsPublic;
            _choice.IsApproved = null;
            return _choice;
        }

        public void DeleteChoice(Choice choice)
        {
            if (!_choices.Contains(choice))
                throw new Exception("Choice is not valid");
            _choices.Remove(choice);
        }

        public void ClearChoices()
        {
            _choices.Clear();
        }

        public int AllChoicesCount => Choices.Count;

        public int CorrectChoicesCount => Choices.Count(c => c.IsCorrect);

    }
}
