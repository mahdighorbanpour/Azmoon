using Abp.Application.Services;
using Azmoon.Admin.Application.Quiz.Interfaces;
using Azmoon.Application.Shared.Quiz.Questions.Dto;
using System;

namespace Azmoon.Admin.Application.Quiz.Questions
{
    public interface IAdminQuestionAppService : IAsyncCrudAppService<QuestionDto, Guid, PagedQuestionResultRequestDto, CreateUpdateQuestionDto, CreateUpdateQuestionDto>
        , IMayBePublicService<Guid>
    {
    }
}
