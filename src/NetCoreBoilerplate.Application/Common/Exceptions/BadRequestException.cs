using System.Net;

namespace NetCoreBoilerplate.Application.Common.Exceptions
{
    public class BadRequestException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;
        public override string Code => "BAD_REQUEST";

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string name, string key)
            : this($"Entity '{name}' invalid ({key}) was found")
        {
        }
    }
}
