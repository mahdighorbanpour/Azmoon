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

        public List<MatchSet> MatchSets { get; set; } = new List<MatchSet>();//=> _matchsets;.AsReadOnly();

        public bool? RandomizeChoices { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public Choice AddChoice(string value, bool isCorrect, int? orderNo = null, string matchSet = null)
        {
            var _matchset = GetMatchSetWithValue(matchSet);
            var choice = new Choice(Id, value, isCorrect, orderNo, _matchset);
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
            _choice.MatchSet = GetMatchSetWithValue(choice.MatchSet?.Value);
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

        public MatchSet AddMatchSet(string value)
        {
            if(QuestionType!= QuestionType.Matching)
                throw new Exception("Please first change the question type to Matching");
            var _matchset = MatchSets.Find(m => string.Compare(m.Value, value, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (_matchset != null)
                throw new Exception("Matchset value already exists!");
            _matchset = new MatchSet();
            _matchset.Value = value;
            _matchset.IsPublic = this.IsPublic;
            _matchset.QuestionId = this.Id;
            MatchSets.Add(_matchset);
            return _matchset;
        }

        public MatchSet UpdateMatchSet(MatchSet matchSet)
        {
            var _matchset = MatchSets.Find(c => c.Id == matchSet.Id);
            if (_matchset == null)
                throw new Exception("MatchSet is not valid");

            _matchset.Value = matchSet.Value;
            _matchset.IsPublic = IsPublic;
            _matchset.IsApproved = null;
            return _matchset;
        }

        public void DeleteMatchSet(MatchSet matchSet)
        {
            if (!MatchSets.Contains(matchSet))
                throw new Exception("MatchSet is not valid");
            MatchSets.Remove(matchSet);
        }

        private MatchSet GetMatchSetWithValue(string value)
        {
            return MatchSets.FirstOrDefault(m => m.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        public int AllChoicesCount => Choices.Count;

        public int CorrectChoicesCount => Choices.Count(c => c.IsCorrect);

    }
}
