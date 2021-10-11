using System.Net;

namespace NetCoreBoilerplate.Application.Common.Exceptions
{
    public class UnauthorizedException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.Unauthorized;
        public override string Code => "UNAUTHORIZED";

        public UnauthorizedException()
            : base()
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }

        public UnauthorizedException(string name, string key)
            : this($"Entity '{name}' invalid key ({key}) was found")
        {
        }
    }
}
