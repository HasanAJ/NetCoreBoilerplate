using System.Net;

namespace NetCoreBoilerplate.Application.Common.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.NotFound;
        public override string Code => "NOT_FOUND";

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string name, string key)
            : this($"Entity '{name}' ({key}) was not found.")
        {
        }
    }
}
