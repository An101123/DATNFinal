using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificResearch.Core.Business.Filters;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Business.Services;

namespace ScientificResearch.Controllers
{
    [Route("api/lecturers")]
    [ValidateModel]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLecturer()
        {
            var lecturer = await _lecturerService.GetAllLecturer();
            return Ok(lecturer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestListViewModel requestListViewModel,DateTime? startTime, DateTime endTime)
        {
            var lecturer = await _lecturerService.ListLecturerAsync(requestListViewModel,startTime, endTime);
            return Ok(lecturer);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetLecturerById(Guid id, DateTime startTime, DateTime endTime)
        //{
        //    var lecturer = await _lecturerService.GetLecturerByIdAsync(id, startTime, endTime);
        //    return Ok(lecturer);
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLecturerById(Guid id,  DateTime startTime, DateTime endTime)
        {
            var lecturer = await _lecturerService.GetLecturerByIdAsync(id, startTime, endTime);
            return Ok(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LecturerManageModel lecturerManageModel)
        {
            var response = await _lecturerService.CreateLecturerAsync(lecturerManageModel);
            return new CustomActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LecturerManageModel lecturerManageModel)
        {
            var response = await _lecturerService.UpdateLecturerAsync(id, lecturerManageModel);
            return new CustomActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _lecturerService.DeleteLecturerAsync(id);
            return new CustomActionResult(response);
        }

        [HttpGet("{id}/scientificWorks")]
        public async Task<IActionResult> GetAllScientificWorkByLecturerId(Guid id, DateTime startTime, DateTime endTime)
        {
            var scientificWorks = await _lecturerService.GetScientificWorkByLecturerIdAsync(id, startTime, endTime);
            return Ok(scientificWorks);
        }

        [HttpGet("{id}/scientificReports")]
        public async Task<IActionResult> GetAllScientificReportByLecturerId(Guid id, DateTime startTime, DateTime endTime)
        {
            var scientificReports = await _lecturerService.GetScientificReportByLecturerIdAsync(id, startTime, endTime);
            return Ok(scientificReports);
        }
        [HttpGet("{id}/publishBooks")]
        public async Task<IActionResult> GetAllPublishBookByLecturerId(Guid id, DateTime startTime, DateTime endTime)
        {
            var publishBooks = await _lecturerService.GetPublishBookByLecturerIdAsync(id, startTime, endTime);
            return Ok(publishBooks);
        }
        [HttpGet("{id}/studyGuides")]
        public async Task<IActionResult> GetAllStudyGuideByLecturerId(Guid id)
        {
            var studyGuides = await _lecturerService.GetStudyGuideByLecturerIdAsync(id);
            return Ok(studyGuides);
        }
        [HttpGet("{id}/otherScientificWorks")]
        public async Task<IActionResult> GetAllOtherScientificWorkByLecturerId(Guid id, DateTime startTime, DateTime endTime)
        {
            var otherScientificWorks = await _lecturerService.GetOtherScientificWorkByLecturerIdAsync(id,  startTime,  endTime);
            return Ok(otherScientificWorks);
        }
    }
}