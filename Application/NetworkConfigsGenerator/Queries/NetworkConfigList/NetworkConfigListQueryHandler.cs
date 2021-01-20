using Application.Common.Filter;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Wrappers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NetworkConfigsGenerator.Queries.NetworkConfigList
{
    public class NetworkConfigListQueryHandler : IRequestHandler<NetworkConfigListQuery, ApiResponseWrapper>
    {
        private readonly INetworkConfigGenerator _networkConfigGenerator;
        private readonly IUriService _uriService;

        public NetworkConfigListQueryHandler(INetworkConfigGenerator networkConfigGenerator,
                                             IUriService uriService)
        {
            _networkConfigGenerator = networkConfigGenerator;
            _uriService = uriService;
        }
        public Task<ApiResponseWrapper> Handle(NetworkConfigListQuery request, CancellationToken cancellationToken)
        {
            var route = request.Path;
            var validFilter = new PaginationFilter(request.Filter.PageNumber, request.Filter.PageSize);
            var pagedData = _networkConfigGenerator
                .GenerateNetworkConfigs(request.NetworkConfigsNumber)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var totalRecords = request.NetworkConfigsNumber;

            var response = PaginationHelper.CreateResponse(pagedData, validFilter, totalRecords, _uriService, route);
            return Task.FromResult(response);

        }
    }
}
