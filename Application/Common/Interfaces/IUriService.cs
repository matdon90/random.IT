using Application.Common.Filter;
using System;

namespace Application.Common.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
