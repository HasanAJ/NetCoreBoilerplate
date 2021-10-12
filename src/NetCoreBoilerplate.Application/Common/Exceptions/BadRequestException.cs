using System.Net;

namespace NetCoreBoilerplate.Application.Common.Exceptions
{
    public class BadRequestException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;
        public override string Code => "BAD_REQUEST";

        public BadRequestException(string key)
            : base($"Invalid '{key}'")
        {
        }
    }
}
