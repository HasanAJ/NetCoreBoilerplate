using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Models;

namespace NetCoreBoilerplate.Application.Common.Interfaces.Services
{
    public interface IMailService
    {
        Task Send(Email email);

        Task Send<T>(Email<T> email) where T : class;
    }
}
