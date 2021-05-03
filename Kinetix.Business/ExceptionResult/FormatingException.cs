using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.Business.ExceptionResult
{
    public class FormatingException : Exception
    {
        public FormatingException()
        {
        }

        public FormatingException(string message)
            : base(message)
        {
        }

        public FormatingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
