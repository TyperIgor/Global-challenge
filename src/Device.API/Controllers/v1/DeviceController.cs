using System.Net;
using Microsoft.AspNetCore.Mvc;
using Device.API.Application.Service.Interfaces;
using Device.API.Application.Message.Dto;

namespace Device.API.Controllers.v1
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
        public async Task<ActionResult<ListDataResponse>> GetAll()
        {
            var result = await _devicesOperation.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<SingleDataResponse>> GetById(Guid id)
        {
            var result = await _devicesOperation.GetById(id);

            if (result is null)
                return NotFound();

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

        [HttpGet("all-by-brand")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetByBrand(string brand)
        {
            var result = await _devicesOperation.GetByBrand(brand);
            return Ok(result);
        }

        [HttpGet("all-by-state")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetByState(int state)
        {
            var result = await _devicesOperation.GetByState(state);
            return Ok(result);
        }

        [HttpPut("update-device")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateFullyOrPartialDevice(DeviceUpdateRequest request)
        {
            var result = await _devicesOperation.PartialOrFullUpdateAsync(request);

            if (result is false)
                return NotFound("Device not exist to be updated");

            return Ok();
        }

        [HttpDelete("delete")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var result = await _devicesOperation.DeleteDeviceAsync(id);

            if (result is false)
                return BadRequest("Device is in use or not exist");

            return NoContent();
        }
    }
}