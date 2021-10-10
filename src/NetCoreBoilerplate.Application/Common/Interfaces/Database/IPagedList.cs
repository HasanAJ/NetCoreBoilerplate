using System.Collections.Generic;

namespace NetCoreBoilerplate.Application.Common.Interfaces.Database
{
    public interface IPagedList<T>
    {
        int Page { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        IEnumerable<T> Items { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
