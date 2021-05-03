using System;

namespace Kinetix.Business.ExceptionResult
{
    public class NoStockException : Exception
    {
        public NoStockException()
        {
        }

        public NoStockException(string message)
            : base(message)
        {
        }

        public NoStockException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
