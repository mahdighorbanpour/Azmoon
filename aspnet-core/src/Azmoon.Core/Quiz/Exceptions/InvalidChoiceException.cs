using System;
using System.Collections.Generic;
using System.Text;

namespace Azmoon.Core.Quiz.Exceptions
{
    public class InvalidChoiceException: Exception
    {
        public InvalidChoiceException(string message): base(message)
        {
        }
    }
}
