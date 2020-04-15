using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificResearch.Core.Business.Filters;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.ClassificationOfScientificWorks;
using ScientificResearch.Core.Business.Models.ClassificationOfScientificWorks;
using ScientificResearch.Core.Business.Services;

namespace ScientificResearch.Controllers
{
    [Route("api/classificationOfOtherScientificWorks")]
    [ValidateModel]
    public class ClassificationOfScientificWorkController : ControllerBase
    {
        private readonly IClassificationOfScientificWorkService _classificationOfOtherScientificWorkService;

        public ClassificationOfScientificWorkController(IClassificationOfScientificWorkService classificationOfOtherScientificWorkService)
        {
            _classificationOfOtherScientificWorkService = classificationOfOtherScientificWorkService;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllClassificationOfScientificWork()
        {
            var classificationOfOtherScientificWork = await _classificationOfOtherScientificWorkService.GetAllClassificationOfScientificWork();
            return Ok(classificationOfOtherScientificWork);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestListViewModel requestListViewModel)
        {
            var classificationOfOtherScientificWork = await _classificationOfOtherScientificWorkService.ListClassificationOfScientificWorkAsync(requestListViewModel);
            return Ok(classificationOfOtherScientificWork);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassificationOfScientificWorkById(Guid id)
        {
            var classificationOfOtherScientificWork = await _classificationOfOtherScientificWorkService.GetClassificationOfScientificWorkByIdAsync(id);
            return Ok(classificationOfOtherScientificWork);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassificationOfScientificWorkManageModel classificationOfOtherScientificWorkManageModel)
        {
            var response = await _classificationOfOtherScientificWorkService.CreateClassificationOfScientificWorkAsync(classificationOfOtherScientificWorkManageModel);
            return new CustomActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ClassificationOfScientificWorkManageModel classificationOfOtherScientificWorkManageModel)
        {
            var response = await _classificationOfOtherScientificWorkService.UpdateClassificationOfScientificWorkAsync(id, classificationOfOtherScientificWorkManageModel);
            return new CustomActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _classificationOfOtherScientificWorkService.DeleteClassificationOfScientificWorkAsync(id);
            return new CustomActionResult(response);
        }

        [HttpGet("{id}/otherScientificWorks")]
        public async Task<IActionResult> GetOtherScientificWorksById(Guid id)
        {
            var classificationOfOtherScientificWork = await _classificationOfOtherScientificWorkService.GetOtherScientificWorkByClassificationOfScientificWorkIdAsync(id);
            return Ok(classificationOfOtherScientificWork);
        }
    }
}