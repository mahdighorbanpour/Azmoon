using System.Collections.Generic;
using System.Threading.Tasks;
using Azmoon.Application.Shared.Quiz;

namespace Azmoon.Admin.Application.Quiz.Interfaces
{
    public interface IHaveDictionary
    {
        Task<List<DictionaryDto>> GetDictionary();
    }
}
