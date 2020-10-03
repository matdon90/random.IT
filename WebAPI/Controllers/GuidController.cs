using Application.GuidGenerator.Queries.GuidSingle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class GuidController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<Guid>> SingleGuid()
        {
            return await Mediator.Send(new GuidSingleQuery());
        }
    }
}
