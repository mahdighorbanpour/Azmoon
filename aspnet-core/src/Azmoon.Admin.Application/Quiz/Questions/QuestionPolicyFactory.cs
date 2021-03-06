﻿using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;

namespace Azmoon.Admin.Application.Questions
{
    public class QuestionPolicyFactory : IQuestionPolicyFactory
    {
        public QuestionPolicyBase CreatePolicy(Question question)
        {
            switch (question.QuestionType)
            {
                case QuestionType.TrueFalse:
                    return new TrueFalseQuestionPolicy(question);
                case QuestionType.MultipleChoice:
                    return new MultipleChoiceQuestionPolicy(question);
                case QuestionType.Ordering:
                    return new OrderingQuestionPolicy(question);
                case QuestionType.ShortAnswer:
                    return new ShortAnswerQuestionPolicy(question);
                case QuestionType.FillInTheBlank:
                    return new FillInTheBlankQuestionPolicy(question);
                case QuestionType.Matching:
                    return new MatchingQuestionPolicy(question);
                default:
                    break;
            }
            return null;
        }
    }
}