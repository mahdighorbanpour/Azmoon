using Abp.Application.Services;
using Azmoon.Application.Shared.Quiz.Quiz.Dto;
using System;

namespace Azmoon.Admin.Application.Quiz.Quiz
{
    public interface IAdminQuizAppService: IAsyncCrudAppService<QuizDto, Guid, PagedQuizResultRequestDto, CreateUpdateQuizDto, CreateUpdateQuizDto>
    {
    }
}
