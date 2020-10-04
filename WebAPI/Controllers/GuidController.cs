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
        /// <returns></returns>
        [HttpGet("{guidNumbers}")]
        public async Task<ActionResult<List<Guid>>> ListGuid(int guidNumbers)
        {
            return await Mediator.Send(new GuidListQuery() { GuidNumbers = guidNumbers });
        }
    }
}
