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
    public class ModelController : Controller
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAll([FromBody] ModelFilterModel filter)
        {
            var result = await _modelService.ReadAll(filter);
            return Ok(result);
        }

        [HttpGet("{modelID:int}")]
        public async Task<IActionResult> Detail(int modelID)
        {
            var result = await _modelService.Read(modelID);
            return Ok(result);
        }

        [HttpGet("lov")]
        public async Task<IActionResult> GetLov()
        {
            var result = await _modelService.ReadLov();
            return Ok(result);
        }

        [HttpDelete("{modelID:int}")]
        public async Task<IActionResult> Delete(int modelID)
        {
            await _modelService.Delete(modelID);
            return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] ModelModel modelModel)
        {
            var result = await _modelService.CreateModel(modelModel);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] ModelModel modelModel)
        {
            await _modelService.Update(modelModel);
            return Ok();
        }
    }
}