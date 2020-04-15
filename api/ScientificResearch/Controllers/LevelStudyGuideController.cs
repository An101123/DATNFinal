using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificResearch.Core.Business.Filters;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.LevelStudyGuides;
using ScientificResearch.Core.Business.Services;

namespace ScientificResearch.Controllers
{
    [Route("api/levelStudyGuides")]
    [ValidateModel]
    public class LevelStudyGuideController : ControllerBase
    {
        private readonly ILevelStudyGuideService _levelStudyGuideService;

        public LevelStudyGuideController(ILevelStudyGuideService levelStudyGuideService)
        {
            _levelStudyGuideService = levelStudyGuideService;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllLevelStudyGuide()
        {
            var levelStudyGuide = await _levelStudyGuideService.GetAllLevelStudyGuide();
            return Ok(levelStudyGuide);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestListViewModel requestListViewModel)
        {
            var levelStudyGuide = await _levelStudyGuideService.ListLevelStudyGuideAsync(requestListViewModel);
            return Ok(levelStudyGuide);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLevelStudyGuideById(Guid id)
        {
            var levelStudyGuide = await _levelStudyGuideService.GetLevelStudyGuideByIdAsync(id);
            return Ok(levelStudyGuide);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LevelStudyGuideManageModel levelStudyGuideManageModel)
        {
            var response = await _levelStudyGuideService.CreateLevelStudyGuideAsync(levelStudyGuideManageModel);
            return new CustomActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LevelStudyGuideManageModel levelStudyGuideManageModel)
        {
            var response = await _levelStudyGuideService.UpdateLevelStudyGuideAsync(id, levelStudyGuideManageModel);
            return new CustomActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _levelStudyGuideService.DeleteLevelStudyGuideAsync(id);
            return new CustomActionResult(response);
        }

        [HttpGet("{id}/studyGuides")]
        public async Task<IActionResult> GetStudyGuidesById(Guid id)
        {
            var levelStudyGuide = await _levelStudyGuideService.GetStudyGuideByLevelStudyGuideIdAsync(id);
            return Ok(levelStudyGuide);
        }
    }
}