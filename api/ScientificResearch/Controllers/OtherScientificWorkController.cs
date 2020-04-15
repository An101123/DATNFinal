using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificResearch.Core.Business.Filters;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.OtherScientificWorks;
using ScientificResearch.Core.Business.Services;

namespace ScientificResearch.Controllers
{
    [Route("api/otherScientificWorks")]
    [ValidateModel]
    public class OtherScientificWorkController : ControllerBase
    {
        private readonly IOtherScientificWorkService _otherScientificWorkService;
        public OtherScientificWorkController(IOtherScientificWorkService otherScientificWorkService)
        {
            _otherScientificWorkService = otherScientificWorkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestListViewModel RequestListViewModel)
        {
            var otherScientificWork = await _otherScientificWorkService.ListOtherScientificWorkAsync(RequestListViewModel);
            return Ok(otherScientificWork);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOtherScientificWorkById(Guid id)
        {
            var classificationOfScientificWork = await _otherScientificWorkService.GetOtherScientificWorkByIdAsync(id);
            return Ok(classificationOfScientificWork);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OtherScientificWorkManageModel otherScientificWorkManagerModel)
        {
            var response = await _otherScientificWorkService.CreateOtherScientificWorkAsync(otherScientificWorkManagerModel);
            return new CustomActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OtherScientificWorkManageModel otherScientificWorkManageModel)
        {
            var response = await _otherScientificWorkService.UpdateOtherScientificWorkAsync(id, otherScientificWorkManageModel);
            return new CustomActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var responseModel = await _otherScientificWorkService.DeleteOtherScientificWorkAsync(id);
            return new CustomActionResult(responseModel);
        }
    }
}