using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificResearch.Core.Business.Filters;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.PublishBooks;
using ScientificResearch.Core.Business.Services;

namespace ScientificResearch.Controllers
{
    [Route("api/publishBooks")]
    [ValidateModel]
    public class PublishBookController : ControllerBase
    {
        private readonly IPublishBookService _publishBookService;
        public PublishBookController(IPublishBookService publishBookService)
        {
            _publishBookService = publishBookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestListViewModel RequestListViewModel)
        {
            var publishBook = await _publishBookService.ListPublishBookAsync(RequestListViewModel);
            return Ok(publishBook);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublishBookById(Guid id)
        {
            var bookCategory = await _publishBookService.GetPublishBookByIdAsync(id);
            return Ok(bookCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PublishBookManageModel publishBookManagerModel)
        {
            var response = await _publishBookService.CreatePublishBookAsync(publishBookManagerModel);
            return new CustomActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PublishBookManageModel publishBookManageModel)
        {
            var response = await _publishBookService.UpdatePublishBookAsync(id, publishBookManageModel);
            return new CustomActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var responseModel = await _publishBookService.DeletePublishBookAsync(id);
            return new CustomActionResult(responseModel);
        }
        [HttpGet("{id}/lecturers")]
        public async Task<IActionResult> GetAllLecturerkByPublishBookId(Guid id)
        {
            var lecturers = await _publishBookService.GetLecturerByPublishBookIdAsync(id);
            return Ok(lecturers);
        }
    }
}