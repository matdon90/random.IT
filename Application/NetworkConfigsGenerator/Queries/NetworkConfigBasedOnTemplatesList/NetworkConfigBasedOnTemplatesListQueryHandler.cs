using Application.Common.Filter;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Wrappers;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NetworkConfigsGenerator.Queries.NetworkConfigBasedOnTemplatesList
{
    public class NetworkConfigBasedOnTemplatesListQueryHandler : IRequestHandler<NetworkConfigBasedOnTemplatesListQuery, ApiResponseWrapper>
    {
        private readonly INetworkConfigGenerator _networkConfigGenerator;
        private readonly IUriService _uriService;

        public NetworkConfigBasedOnTemplatesListQueryHandler(INetworkConfigGenerator networkConfigGenerator,
                                             IUriService uriService)
        {
            _networkConfigGenerator = networkConfigGenerator;
            _uriService = uriService;
        }

        public Task<ApiResponseWrapper> Handle(NetworkConfigBasedOnTemplatesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<NetworkConfig> data;

            var route = request.Path;
            var validFilter = new PaginationFilter(request.Filter.PageNumber, request.Filter.PageSize);

            if (request.IpTemplate != null && request.SubnetMask != null)
            {
                data = _networkConfigGenerator.GenerateNetworkConfigsByIpAndMaskTemplate(request.NetworkConfigsNumber, request.IpTemplate, request.SubnetMask);
            }
            else if (request.IpTemplate != null)
            {
                data = _networkConfigGenerator.GenerateNetworkConfigsByIpTemplate(request.NetworkConfigsNumber, request.IpTemplate);
            }
            else if (request.SubnetMask != null)
            {
                data = _networkConfigGenerator.GenerateNetworkConfigsByMask(request.NetworkConfigsNumber, request.SubnetMask);
            }
            else
            {
                data = _networkConfigGenerator.GenerateNetworkConfigs(request.NetworkConfigsNumber);
            }
            
            var pagedData = data
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var totalRecords = request.NetworkConfigsNumber;

            var response = PaginationHelper.CreateResponse(pagedData, validFilter, totalRecords, _uriService, route);
            return Task.FromResult(response);
        }
    }
}
