using System.Net;
using Microsoft.AspNetCore.Mvc;
using Device.API.Application.Service.Interfaces;
using Device.API.Application.Message.Dto;

namespace Device.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeviceController(IDevicesOperation devicesOperation) : Controller
    {
        private readonly IDevicesOperation _devicesOperation = devicesOperation;

        [HttpGet("all")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<List<DeviceResponse>>> GetAll()
        {
            var result = await _devicesOperation.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<List<DeviceResponse>>> GetById(Guid id)
        {
            var result = await _devicesOperation.GetById(id);

            return Ok(result);
        }


        [HttpPost("add")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Create(string name, string brand)
        {
           var result = await _devicesOperation.CreateAsync(name, brand);
           return Created();
        }

        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var result = await _devicesOperation.DeleteDeviceAsync(id);
            return NoContent();
        }
    }
}
