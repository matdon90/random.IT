using Application.Common.Filter;
using Application.Common.Wrappers;
using Application.GuidGenerator.Queries.GuidList;
using Application.GuidGenerator.Queries.GuidSingle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class GuidController : ApiController
    {
        /// <summary>
        /// Returns single GUID generated based on current timedate.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Guid>> SingleGuid()
        {
            return await Mediator.Send(new GuidSingleQuery());
        }

        /// <summary>
        /// Return list with number of GUIDs declared in parameter. GUIDs generated based on current timedate.
        /// </summary>
        /// <param name="guidNumbers"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("{guidNumbers}")]
        public async Task<ActionResult<ApiResponseWrapper>> ListGuid(int guidNumbers, [FromQuery] PaginationFilter filter)
        {
            return await Mediator.Send(new GuidListQuery(guidNumbers, filter, Request.Path.Value));
        }
    }
}