using Application.Common.Filter;
using Application.Common.Wrappers;
using Application.NetworkConfigsGenerator.Queries.NetworkConfigBasedOnTemplatesList;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class NetworkConfigController : ApiController
    {
        /// <summary>
        /// Return list with number of random network configs declared in parameter. Can be parametrized with use of IP address template or/and subnet mask. 
        /// </summary>
        /// <param name="networkConfigsNumber">Number of network configs</param>
        /// <param name="filter">Pagination filter</param>
        /// <param name="ipTemplate" ex>IP address template</param>
        /// <param name="subnetMask">Subnet mask</param>
        /// <returns>List of random network configs</returns>
        [HttpGet("{networkConfigsNumber}")]
        public async Task<ActionResult<ApiResponseWrapper>> ListNetworkConfigsWithIpTemplate(int networkConfigsNumber, [FromQuery] PaginationFilter filter, [FromQuery] string ipTemplate = null, [FromQuery] string subnetMask = null)
        {
            return await Mediator.Send(new NetworkConfigBasedOnTemplatesListQuery(networkConfigsNumber, ipTemplate, subnetMask, filter, Request.Path.Value));
        }
    }
}
