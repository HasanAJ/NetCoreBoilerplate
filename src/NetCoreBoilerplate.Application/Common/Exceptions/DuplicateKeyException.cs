using System.Net;

namespace NetCoreBoilerplate.Application.Common.Exceptions
{
    public class DuplicateKeyException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.NotFound;
        public override string Code => "DUPLCAITE_KEY";

        public DuplicateKeyException(string message)
            : base(message)
        {
        }

        public DuplicateKeyException(string name, string key)
            : this($"Entity '{name}' duplicate key ({key}) was found")
        {
        }
    }
}
