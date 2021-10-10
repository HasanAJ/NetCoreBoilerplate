using System;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;

namespace NetCoreBoilerplate.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
