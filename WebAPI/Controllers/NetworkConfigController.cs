using Application.Common.Filter;
using Application.Common.Wrappers;
using Application.NetworkConfigsGenerator.Queries.NetworkConfigList;
using Application.NetworkConfigsGenerator.Queries.NetworkConfigListWithIpTemplate;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class NetworkConfigController : ApiController
    {
        [HttpGet("{networkConfigsNumber}")]
        public async Task<ActionResult<ApiResponseWrapper>> ListNetworkConfigs(int networkConfigsNumber, [FromQuery] PaginationFilter filter)
        {
            return await Mediator.Send(new NetworkConfigListQuery(networkConfigsNumber, filter, Request.Path.Value));
        }

        [HttpGet("{networkConfigsNumber}/{ipTemplate}")]
        public async Task<ActionResult<ApiResponseWrapper>> ListNetworkConfigsWithIpTemplate(int networkConfigsNumber, string ipTemplate, [FromQuery] PaginationFilter filter)
        {
            return await Mediator.Send(new NetworkConfigWithIpTemplateListQuery(networkConfigsNumber, ipTemplate, filter, Request.Path.Value));
        }
    }
}
