using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.PublishBooks;
using ScientificResearch.Core.Business.Reflections;
using ScientificResearch.Core.Common.Constants;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Core.Entities;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Services
{
   
    public interface IPublishBookService
    {
        Task<PagedList<PublishBookViewModel>> ListPublishBookAsync(RequestListViewModel requestListViewModel);

        Task<PublishBookViewModel> GetPublishBookByIdAsync(Guid? id);

        Task<ResponseModel> CreatePublishBookAsync(PublishBookManageModel publishBookManagerModel);

        Task<ResponseModel> UpdatePublishBookAsync(Guid id, PublishBookManageModel publishBookManagerModel);

        Task<ResponseModel> DeletePublishBookAsync(Guid id);
    }
    public class PublishBookService : IPublishBookService
    {
        private readonly IRepository<PublishBook> _publishBookResponstory;
        private readonly IRepository<BookCategory> _bookCategoryRepository;
        private readonly IRepository<Lecturer> _lecturerRepository;
        private readonly IMapper _mapper;

        public PublishBookService(IRepository<PublishBook> publishBookResponstory, IRepository<BookCategory> bookCategoryRepository, IRepository<Lecturer> lecturerRepository, IMapper mapper)
        {
            _publishBookResponstory = publishBookResponstory;
            _bookCategoryRepository = bookCategoryRepository;
            _lecturerRepository = lecturerRepository;
            _mapper = mapper;
        }

        #region private method
        private IQueryable<PublishBook> GetAll()
        {
            return _publishBookResponstory.GetAll().Include(x => x.BookCategory)
                .Include(x => x.Lecturer);
        }

        private List<string> GetAllPropertyNameOfPublishBookViewModel()
        {
            var publishBookViewModel = new PublishBookViewModel();

            var type = publishBookViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
        #endregion
        public async Task<PagedList<PublishBookViewModel>> ListPublishBookAsync(RequestListViewModel requestListViewModel)
        {
            var list = await GetAll()
            .Where(x => (!requestListViewModel.IsActive.HasValue || x.RecordActive == requestListViewModel.IsActive)
            && (string.IsNullOrEmpty(requestListViewModel.Query)
                || (x.Name.Contains(requestListViewModel.Query)
                || (x.BookCategory.Name.Contains(requestListViewModel.Query))
                || (x.Lecturer.Name.Contains(requestListViewModel.Query))
                )))
            .Select(x => new PublishBookViewModel(x)).ToListAsync();

            var publishBookViewModelProperties = GetAllPropertyNameOfPublishBookViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(requestListViewModel.SortName) ? requestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var publishBookViewModelProperty in publishBookViewModelProperties)
            {
                var lowerTypeViewModelProperty = publishBookViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = publishBookViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(PublishBookViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = requestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<PublishBookViewModel>(list, requestListViewModel.Offset ?? CommonConstants.Config.DEFAULT_SKIP, requestListViewModel.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<PublishBookViewModel> GetPublishBookByIdAsync(Guid? id)
        {
            var publishBook = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            return new PublishBookViewModel(publishBook);
        }

        public async Task<ResponseModel> DeletePublishBookAsync(Guid id)
        {
            return await _publishBookResponstory.DeleteAsync(id);
        }

        public async Task<ResponseModel> CreatePublishBookAsync(PublishBookManageModel publishBookManageModel)
        {
            var publishBook = await _publishBookResponstory.FetchFirstAsync(x => x.Name == publishBookManageModel.Name && x.BookCategoryId == publishBookManageModel.BookCategoryId);
            if (publishBook != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "This PublishBook is exist. Can you try again with the update!"
                };
            }
            else
            {
                var bookCategory = await _bookCategoryRepository.GetByIdAsync(publishBookManageModel.BookCategoryId);
                var lecturer = await _lecturerRepository.GetByIdAsync(publishBookManageModel.LecturerId);
                publishBook = _mapper.Map<PublishBook>(publishBookManageModel);
                publishBook.BookCategory = bookCategory;
                publishBook.Lecturer = lecturer;

                await _publishBookResponstory.InsertAsync(publishBook);
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new PublishBookViewModel(publishBook)
                };
            }
        }
        public async Task<ResponseModel> UpdatePublishBookAsync(Guid id, PublishBookManageModel publishBookManageModel)
        {
            var publishBook = await _publishBookResponstory.GetByIdAsync(id);
            if (publishBook == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This PublishBook is not exist. Please try again!"
                };
            }
            else
            {
                var existedPublishBook = await _publishBookResponstory.FetchFirstAsync(x => x.Name == publishBookManageModel.Name && x.BookCategoryId == publishBookManageModel.BookCategoryId && x.Id != id);
                if (existedPublishBook != null)
                {
                    var bookCategory = await _publishBookResponstory.GetByIdAsync(publishBookManageModel.BookCategoryId);
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "PublishBook " + existedPublishBook.Name + " already created on BookCategory " + bookCategory.Name,
                    };
                }
                else
                {
                    publishBookManageModel.GetPublishBookFromModel(publishBook);
                    return await _publishBookResponstory.UpdateAsync(publishBook);
                }
            }

        }

    }
}
