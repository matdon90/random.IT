using Application.Common.Filter;
using Application.Common.Wrappers;
using MediatR;

namespace Application.NetworkConfigsGenerator.Queries.NetworkConfigListWithIpTemplate
{
    public class NetworkConfigWithIpTemplateListQuery : IRequest<ApiResponseWrapper>
    {
        public int NetworkConfigsNumber { get; set; }
        public string IpTemplate { get; set; }
        public PaginationFilter Filter { get; set; }
        public string Path { get; set; }

        public NetworkConfigWithIpTemplateListQuery(int numberOfNetowrkConfigs, string ipTemplate, PaginationFilter filter, string path)
        {
            NetworkConfigsNumber = numberOfNetowrkConfigs;
            IpTemplate = ipTemplate;
            Filter = filter;
            Path = path;
        }
    }
}
