using System.Net;
using Microsoft.AspNetCore.Mvc;
using Device.API.Application.Service.Interfaces;
using Device.API.Application.Message.Dto;

namespace Device.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly IDevicesOperation devicesOperation;

        public DeviceController()
        {

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<DeviceResponse>>> GetAll()
        {
            return new List<DeviceResponse>();
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public string GeById(int id)
        {
            return "value";
        }
    }
}
