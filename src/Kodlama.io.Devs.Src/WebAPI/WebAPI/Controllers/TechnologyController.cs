using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetListTechnologyByDynamic;
using Application.Features.Technologies.Queries.GetlListTechonlogy;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TechnologyController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechologyQuery getListTechologyQuery = new GetListTechologyQuery { PageRequest = pageRequest };

            TechonlogyListModel result = await Mediator.Send(getListTechologyQuery);

            return Ok(result);


        }


        [HttpPost]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListTechnologyByDynamicQuery getListTechnologyByDynamicQuery = new GetListTechnologyByDynamicQuery { PageRequest = pageRequest ,Dynamic = dynamic};

            TechonlogyListModel result = await Mediator.Send(getListTechnologyByDynamicQuery);

            return Ok(result);


        }
    }
}
