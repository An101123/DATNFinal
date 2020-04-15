using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.BookCategorys;
using ScientificResearch.Core.Business.Models.PublishBooks;
using ScientificResearch.Core.Business.Reflections;
using ScientificResearch.Core.Common.Constants;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Services
{
    public interface IBookCategoryService
    {
        Task<List<BookCategoryViewModel>> GetAllBookCategory();
        Task<PagedList<BookCategoryViewModel>> ListBookCategoryAsync(RequestListViewModel requestListViewModel);
        Task<BookCategory> GetBookCategoryByIdAsync(Guid? id);
        Task<ResponseModel> CreateBookCategoryAsync(BookCategoryManageModel bookCategoryManagerModel);
        Task<ResponseModel> UpdateBookCategoryAsync(Guid id, BookCategoryManageModel bookCategoryManagerModel);
        Task<ResponseModel> DeleteBookCategoryAsync(Guid id);
        Task<ResponseModel> GetPublishBookByBookCategoryIdAsync(Guid? id);
    }
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IRepository<BookCategory> _repository;
        private readonly IMapper _mapper;

        public BookCategoryService(IRepository<BookCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region private method


        private IQueryable<BookCategory> GetAll()
        {
            return _repository.GetAll().Where(i => !i.RecordDeleted);
        }

        private List<string> GetAllPropertyNameOfBookCategoryViewModel()
        {
            var bookCategoryViewModel = new BookCategoryViewModel();

            var type = bookCategoryViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }

        #endregion
        public async Task<List<BookCategoryViewModel>> GetAllBookCategory()
        {
            var list = await GetAll().Select(x => new BookCategoryViewModel(x)).ToListAsync();
            return list;
        }

        public async Task<PagedList<BookCategoryViewModel>> ListBookCategoryAsync(RequestListViewModel requestListViewModel)
        {
            var list = await GetAll()
                .Where(x => (!requestListViewModel.IsActive.HasValue || x.RecordActive == requestListViewModel.IsActive)
                && (string.IsNullOrEmpty(requestListViewModel.Query)
                    || (x.Name.Contains(requestListViewModel.Query)
                    )))
                .Select(x => new BookCategoryViewModel(x)).ToListAsync();

            var bookCategoryViewModelProperties = GetAllPropertyNameOfBookCategoryViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(requestListViewModel.SortName) ? requestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var bookCategoryViewModelProperty in bookCategoryViewModelProperties)
            {
                var lowerTypeViewModelProperty = bookCategoryViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = bookCategoryViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(BookCategoryViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = requestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<BookCategoryViewModel>(list, requestListViewModel.Offset ?? CommonConstants.Config.DEFAULT_SKIP, requestListViewModel.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<BookCategory> GetBookCategoryByIdAsync(Guid? id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResponseModel> CreateBookCategoryAsync(BookCategoryManageModel bookCategoryManageModel)
        {
            var bookCategory = await _repository.FetchFirstAsync(x => x.Name == bookCategoryManageModel.Name);
            if (bookCategory != null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "This BookCategory is exist"
                };
            }
            else
            {
                bookCategory = new BookCategory();
                bookCategoryManageModel.GetBookCategoryFromModel(bookCategory);
                await _repository.InsertAsync(bookCategory);
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new BookCategoryViewModel(bookCategory),
                };
            }
        }

        public async Task<ResponseModel> UpdateBookCategoryAsync(Guid id, BookCategoryManageModel bookCategoryManageModel)
        {
            var bookCategory = await _repository.GetByIdAsync(id);
            if (bookCategory == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This bookCategory is not exist"
                };
            }
            else
            {
                var existedBookCategoryName = await _repository.FetchFirstAsync(x => x.Name == bookCategoryManageModel.Name && x.Id != id);
                if (existedBookCategoryName != null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "BookCategory " + bookCategoryManageModel.Name + " is exist on system. Please try again!",
                    };
                }
                else
                {
                    bookCategoryManageModel.GetBookCategoryFromModel(bookCategory);
                    return await _repository.UpdateAsync(bookCategory);
                }
            }
        }

        public async Task<ResponseModel> DeleteBookCategoryAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ResponseModel> GetPublishBookByBookCategoryIdAsync(Guid? id)
        {
            var bookCategory = await GetAll()
                .Include(x => x.PublishBooks)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (bookCategory.PublishBooks == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This bookCategory has no ScientificWork"
                };
            }
            else
            {
                List<PublishBookViewModel> scientificWorks = bookCategory.PublishBooks.Select(x => new PublishBookViewModel(x)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = scientificWorks
                };
            }
        }

    }
}
