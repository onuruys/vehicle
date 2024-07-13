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
            var result = await _vehicleService.CreateVehicle(vehicleModel);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] VehicleModel vehicleModel)
        {
            await _vehicleService.Update(vehicleModel);
            return Ok();
        }
    }
}