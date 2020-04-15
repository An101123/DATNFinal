using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificResearch.Core.Business.Filters;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.BookCategorys;
using ScientificResearch.Core.Business.Services;

namespace ScientificResearch.Controllers
{
    [Route("api/bookCategorys")]
    [ValidateModel]
    public class BookCategoryController : ControllerBase
    {
        private readonly IBookCategoryService _bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService)
        {
            _bookCategoryService = bookCategoryService;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookCategory()
        {
            var bookCategory = await _bookCategoryService.GetAllBookCategory();
            return Ok(bookCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestListViewModel requestListViewModel)
        {
            var bookCategory = await _bookCategoryService.ListBookCategoryAsync(requestListViewModel);
            return Ok(bookCategory);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookCategoryById(Guid id)
        {
            var bookCategory = await _bookCategoryService.GetBookCategoryByIdAsync(id);
            return Ok(bookCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCategoryManageModel bookCategoryManageModel)
        {
            var response = await _bookCategoryService.CreateBookCategoryAsync(bookCategoryManageModel);
            return new CustomActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookCategoryManageModel bookCategoryManageModel)
        {
            var response = await _bookCategoryService.UpdateBookCategoryAsync(id, bookCategoryManageModel);
            return new CustomActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _bookCategoryService.DeleteBookCategoryAsync(id);
            return new CustomActionResult(response);
        }

        [HttpGet("{id}/scientificWorks")]
        public async Task<IActionResult> GetPublishBooksById(Guid id)
        {
            var bookCategory = await _bookCategoryService.GetPublishBookByBookCategoryIdAsync(id);
            return Ok(bookCategory);
        }
    }
}