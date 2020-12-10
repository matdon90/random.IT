using Application.Common.Filter;
using Application.Common.Interfaces;
using Application.Common.Wrappers;
using System;

namespace Application.Common.Helpers
{
    public static class PaginationHelper
    {
        public static ApiResponseWrapper CreateResponse(
                object data,
                PaginationFilter validFilter,
                int totalRecords,
                IUriService uriService,
                string route
            )
        {
            var respose = new ApiResponseWrapper(data, new Pagination(validFilter));

            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            respose.Pagination.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;

            respose.Pagination.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;

            respose.Pagination.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            respose.Pagination.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            respose.Pagination.TotalPages = roundedTotalPages;
            respose.Pagination.TotalRecords = totalRecords;

            return respose;
        }
    }
}
