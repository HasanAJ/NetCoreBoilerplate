using System.Collections.Generic;
using System.Net;

namespace NetCoreBoilerplate.Application.Common.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;
        public override string Code => "FAILED_VALIDATION";

        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : this()
        {
            Errors = errors;
        }

    }
}
