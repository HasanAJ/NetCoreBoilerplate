using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Application.Common.Interfaces.Services
{
    public interface IContext
    {
        string UserId { get; }
        Account Account { get; }
    }
}
