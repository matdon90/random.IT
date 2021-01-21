using Application.Common.Filter;
using Application.Common.Wrappers;
using MediatR;

namespace Application.NetworkConfigsGenerator.Queries.NetworkConfigBasedOnTemplatesList
{
    public class NetworkConfigBasedOnTemplatesListQuery : IRequest<ApiResponseWrapper>
    {
        public int NetworkConfigsNumber { get; set; }
        public string IpTemplate { get; set; }
        public string SubnetMask { get; set; }
        public PaginationFilter Filter { get; set; }
        public string Path { get; set; }

        public NetworkConfigBasedOnTemplatesListQuery(int numberOfNetowrkConfigs, string ipTemplate, string subnetMask, PaginationFilter filter, string path)
        {
            NetworkConfigsNumber = numberOfNetowrkConfigs;
            IpTemplate = ipTemplate;
            SubnetMask = subnetMask;
            Filter = filter;
            Path = path;
        }
    }
}
