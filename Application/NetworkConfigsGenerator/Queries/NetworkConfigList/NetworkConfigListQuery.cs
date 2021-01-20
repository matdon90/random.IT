using Application.Common.Filter;
using Application.Common.Wrappers;
using MediatR;

namespace Application.NetworkConfigsGenerator.Queries.NetworkConfigList
{
    public class NetworkConfigListQuery : IRequest<ApiResponseWrapper>
    {
        public int NetworkConfigsNumber { get; set; }
        public PaginationFilter Filter { get; set; }
        public string Path { get; set; }

        public NetworkConfigListQuery(int numberOfNetowrkConfigs, PaginationFilter filter, string path)
        {
            NetworkConfigsNumber = numberOfNetowrkConfigs;
            Filter = filter;
            Path = path;
        }
    }
}
