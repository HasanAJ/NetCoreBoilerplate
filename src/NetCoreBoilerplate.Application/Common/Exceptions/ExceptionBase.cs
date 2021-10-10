using System;

namespace NetCoreBoilerplate.Application.Common.Exceptions
{
    public class ExceptionBase : Exception
    {
        public virtual int StatusCode { get; set; }
        public virtual string Code { get; set; }

        public ExceptionBase()
        {
        }

        public ExceptionBase(string message)
            : base(message)
        {
        }

        public ExceptionBase(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
