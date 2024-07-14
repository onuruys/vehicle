using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleAPI.Models;
using VehicleAPI.Services;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAll([FromBody] VehicleFilterModel filter)
        {
            var result = await _vehicleService.ReadAll(filter);
            return Ok(result);
        }

        [HttpGet("{vehicleID:int}")]
        public async Task<IActionResult> Detail(int vehicleID)
        {
            var result = await _vehicleService.Read(vehicleID);
            return Ok(result);
        }

        [HttpGet("lov")]
        public async Task<IActionResult> GetLov()
        {
            var result = await _vehicleService.ReadLov();
            return Ok(result);
        }

        [HttpDelete("{vehicleID:int}")]
        public async Task<IActionResult> Delete(int vehicleID)
        {
            await _vehicleService.Delete(vehicleID);
            return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] VehicleModel vehicleModel)
        {
            try
            {
                var result = await _vehicleService.CreateVehicle(vehicleModel);
                return Ok(new  { data = result, success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("INVALID_PLATE"))
                {
                    return BadRequest(new { Message = "Plate format is invalid.", success = false});
                }
                if (ex.Message.Contains("INVALID_MODEL_YEAR"))
                {
                    return BadRequest(new { Message = "Model year must be greater than 1900", success = false });
                }
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { Message = "An unexpected error occurred while creating the vehicle.", success = false });
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] VehicleModel vehicleModel)
        {
            await _vehicleService.Update(vehicleModel);
            return Ok();
        }
    }
}