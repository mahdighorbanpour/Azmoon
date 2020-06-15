using AutoMapper;
using Azmoon.Application.Shared.Quiz.Questions.Dto;
using Azmoon.Core.Quiz.Entities;
using System;
using System.Linq;

namespace Azmoon.Application.Shared.Mappings.Converters
{
    public class CreateUpdateQuestionConverter : ITypeConverter<CreateUpdateQuestionDto, Question>
    {
        public Question Convert(CreateUpdateQuestionDto source, Question destination, ResolutionContext context)
        {
            if (destination == null)
                destination = new Question();
            destination.Id = source.Id;
            destination.CategoryId = source.CategoryId;
            destination.QuestionType = source.QuestionType;
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.Hint = source.Hint;
            destination.IsPublic = source.IsPublic;
            destination.Marks = source.Marks;
            destination.RandomizeChoices = source.RandomizeChoices;
            
            var oldMatchSetIds = destination.MatchSets.Select(m => m.Id).ToList();
            var newMatchSetIds = source.MatchSets.Select(c => c.Id).ToList();
            foreach (var matchSetId in oldMatchSetIds.Where(c => !newMatchSetIds.Contains(c)))
                destination.DeleteMatchSet(destination.MatchSets.FirstOrDefault(c => c.Id == matchSetId));

            // add or update matchsets
            foreach (var matchSet in source.MatchSets)
            {
                if (matchSet.Id == Guid.Empty)
                {
                    destination.AddMatchSet(matchSet.Value);
                }
                else
                {
                    
                    destination.UpdateMatchSet(context.Mapper.Map<MatchSet>(matchSet));
                }
            }

            // delete choices that are removed
            //var oldChoicesIds = (await GetChoicesForQuestion(source.Id)).Select(c => c.Id).ToList();
            var oldChoicesIds = destination.Choices.Select(c => c.Id).ToList();
            var newChoicesIds = source.Choices.Select(c => c.Id).ToList();
            foreach (var choiceId in oldChoicesIds.Where(c => !newChoicesIds.Contains(c)))
                destination.DeleteChoice(destination.Choices.FirstOrDefault(c => c.Id == choiceId));

            // add or update choices
            foreach (var choice in source.Choices)
            {
                Choice newChoice;
                if (choice.Id == Guid.Empty)
                {
                    newChoice = destination.AddChoice(choice.Value, choice.IsCorrect, choice.OrderNo, choice.MatchSet?.Value);
                }
                else
                {
                    newChoice = destination.UpdateChoice(context.Mapper.Map<Choice>(choice));
                }
                while (newChoice.Blanks.Count > 0)
                {
                    newChoice.Blanks.RemoveAt(0);
                }
                foreach (var blank in choice.Blanks)
                    newChoice.AddBlank(blank.Index, blank.Answer);
            }
            return destination;
        }
    }
}
