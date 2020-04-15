using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificResearch.Core.Business.Filters;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.StudyGuides;
using ScientificResearch.Core.Business.Services;

namespace ScientificResearch.Controllers
{
    [Route("api/studyGuides")]
    [ValidateModel]
    public class StudyGuideController : ControllerBase
    {
        private readonly IStudyGuideService _studyGuideService;
        public StudyGuideController(IStudyGuideService studyGuideService)
        {
            _studyGuideService = studyGuideService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestListViewModel RequestListViewModel)
        {
            var studyGuide = await _studyGuideService.ListStudyGuideAsync(RequestListViewModel);
            return Ok(studyGuide);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudyGuideById(Guid id)
        {
            var levelStudyGuide = await _studyGuideService.GetStudyGuideByIdAsync(id);
            return Ok(levelStudyGuide);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudyGuideManageModel studyGuideManagerModel)
        {
            var response = await _studyGuideService.CreateStudyGuideAsync(studyGuideManagerModel);
            return new CustomActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StudyGuideManageModel studyGuideManageModel)
        {
            var response = await _studyGuideService.UpdateStudyGuideAsync(id, studyGuideManageModel);
            return new CustomActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var responseModel = await _studyGuideService.DeleteStudyGuideAsync(id);
            return new CustomActionResult(responseModel);
        }
    }
}