﻿using Application.Common.Filter;
using Application.Common.Wrappers;
using MediatR;

namespace Application.GuidGenerator.Queries.GuidList
{
    public class GuidListQuery : IRequest<ApiResponseWrapper>
    {
        public int GuidNumbers { get; set; }
        public bool IsUppercase { get; set; }
        public PaginationFilter Filter { get; set; }
        public string Path { get; set; }

        public GuidListQuery(int numberOfGuids, bool isUppercase, PaginationFilter filter, string path)
        {
            GuidNumbers = numberOfGuids;
            IsUppercase = isUppercase;
            Filter = filter;
            Path = path;
        }
    }
}
