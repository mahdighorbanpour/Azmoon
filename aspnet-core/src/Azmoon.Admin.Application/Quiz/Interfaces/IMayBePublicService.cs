using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application.Quiz.Interfaces
{
    public interface IMayBePublicService<TPrimaryKey>
    {
        Task ApproveIsPublic(TPrimaryKey id);
        Task RejectIsPublic(TPrimaryKey id);
        Task ResetIsPublic(TPrimaryKey id);
    }
}
