using System;
using System.Collections.Generic;
using System.Text;

namespace Azmoon.Core.Quiz.Interfaces
{
    public interface INeedHostApproval
    {
        bool? IsApproved { get; set; }
    }
}
