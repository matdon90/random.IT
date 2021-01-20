using Application.Common.Filter;
using Application.Common.Wrappers;
using Application.NetworkConfigsGenerator.Queries.NetworkConfigList;
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
    }
}
