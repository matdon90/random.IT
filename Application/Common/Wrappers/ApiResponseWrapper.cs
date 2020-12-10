using Application.Common.Filter;
using System;

namespace Application.Common.Wrappers
{
    public class ApiResponseWrapper
    {
        #region PROPS

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public Pagination Pagination { get; set; }

        #endregion

        #region CONSTRUCTORS

        public ApiResponseWrapper() {}
        public ApiResponseWrapper(object data)
        {
            StatusCode = 200;
            Result = data;
        }

        public ApiResponseWrapper(object data = null,
                                  Pagination pagination = null)
        {
            StatusCode = 200;
            Message = "GET Request successful.";
            Result = data;
            Pagination = pagination;
        }

        public ApiResponseWrapper(object data = null,
                               string message = "",
                               int statusCode = 200,
                               Pagination pagination = null)
        {
            StatusCode = statusCode;
            Message = message == string.Empty ? "GET Request successful." : message;
            Result = data;
            Pagination = pagination;
        }

        #endregion
    }

    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }

        public Pagination() {}

        public Pagination(PaginationFilter validFilter)
        {
            PageNumber = validFilter.PageNumber;
            PageSize = validFilter.PageSize;
        }
    }
}
