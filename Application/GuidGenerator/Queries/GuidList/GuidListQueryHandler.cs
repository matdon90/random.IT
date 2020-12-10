using Application.Common.Filter;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Wrappers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GuidGenerator.Queries.GuidList
{
    public class GuidListQueryHandler : IRequestHandler<GuidListQuery, ApiResponseWrapper>
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IUriService _uriService;

        public GuidListQueryHandler(IGuidGenerator guidGenerator,
                                    IUriService uriService)
        {
            _guidGenerator = guidGenerator;
            _uriService = uriService;
        }

        public Task<ApiResponseWrapper> Handle(GuidListQuery request, CancellationToken cancellationToken)
        {
            var route = request.Path;
            var validFilter = new PaginationFilter(request.Filter.PageNumber, request.Filter.PageSize);
            var pagedData = _guidGenerator
                .GuidGenerateMultiple(request.GuidNumbers)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var totalRecords = request.GuidNumbers;

            var response = PaginationHelper.CreateResponse(pagedData, validFilter, totalRecords, _uriService, route);
            return Task.FromResult(response);
        }
    }
}
