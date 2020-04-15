using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.ClassificationOfScientificWorks;
using ScientificResearch.Core.Business.Models.OtherScientificWorks;
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
    public interface IClassificationOfScientificWorkService
    {
        Task<List<ClassificationOfScientificWorkViewModel>> GetAllClassificationOfScientificWork();
        Task<PagedList<ClassificationOfScientificWorkViewModel>> ListClassificationOfScientificWorkAsync(RequestListViewModel requestListViewModel);
        Task<ClassificationOfScientificWork> GetClassificationOfScientificWorkByIdAsync(Guid? id);
        Task<ResponseModel> CreateClassificationOfScientificWorkAsync(ClassificationOfScientificWorkManageModel classificationOfScientificWorkManagerModel);
        Task<ResponseModel> UpdateClassificationOfScientificWorkAsync(Guid id, ClassificationOfScientificWorkManageModel classificationOfScientificWorkManagerModel);
        Task<ResponseModel> DeleteClassificationOfScientificWorkAsync(Guid id);
        Task<ResponseModel> GetOtherScientificWorkByClassificationOfScientificWorkIdAsync(Guid? id);
    }
    public class ClassificationOfScientificWorkService : IClassificationOfScientificWorkService
    {
        private readonly IRepository<ClassificationOfScientificWork> _repository;
        private readonly IMapper _mapper;

        public ClassificationOfScientificWorkService(IRepository<ClassificationOfScientificWork> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region private method


        private IQueryable<ClassificationOfScientificWork> GetAll()
        {
            return _repository.GetAll().Where(i => !i.RecordDeleted);
        }

        private List<string> GetAllPropertyNameOfClassificationOfScientificWorkViewModel()
        {
            var classificationOfScientificWorkViewModel = new ClassificationOfScientificWorkViewModel();

            var type = classificationOfScientificWorkViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }

        #endregion
        public async Task<List<ClassificationOfScientificWorkViewModel>> GetAllClassificationOfScientificWork()
        {
            var list = await GetAll().Select(x => new ClassificationOfScientificWorkViewModel(x)).ToListAsync();
            return list;
        }

        public async Task<PagedList<ClassificationOfScientificWorkViewModel>> ListClassificationOfScientificWorkAsync(RequestListViewModel requestListViewModel)
        {
            var list = await GetAll()
                .Where(x => (!requestListViewModel.IsActive.HasValue || x.RecordActive == requestListViewModel.IsActive)
                && (string.IsNullOrEmpty(requestListViewModel.Query)
                    || (x.Name.Contains(requestListViewModel.Query)
                    )))
                .Select(x => new ClassificationOfScientificWorkViewModel(x)).ToListAsync();

            var classificationOfScientificWorkViewModelProperties = GetAllPropertyNameOfClassificationOfScientificWorkViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(requestListViewModel.SortName) ? requestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var classificationOfScientificWorkViewModelProperty in classificationOfScientificWorkViewModelProperties)
            {
                var lowerTypeViewModelProperty = classificationOfScientificWorkViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = classificationOfScientificWorkViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(ClassificationOfScientificWorkViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = requestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<ClassificationOfScientificWorkViewModel>(list, requestListViewModel.Offset ?? CommonConstants.Config.DEFAULT_SKIP, requestListViewModel.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<ClassificationOfScientificWork> GetClassificationOfScientificWorkByIdAsync(Guid? id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResponseModel> CreateClassificationOfScientificWorkAsync(ClassificationOfScientificWorkManageModel classificationOfScientificWorkManageModel)
        {
            var classificationOfScientificWork = await _repository.FetchFirstAsync(x => x.Name == classificationOfScientificWorkManageModel.Name);
            if (classificationOfScientificWork != null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "This ClassificationOfScientificWork is exist"
                };
            }
            else
            {
                classificationOfScientificWork = new ClassificationOfScientificWork();
                classificationOfScientificWorkManageModel.GetClassificationOfScientificWorkFromModel(classificationOfScientificWork);
                await _repository.InsertAsync(classificationOfScientificWork);
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new ClassificationOfScientificWorkViewModel(classificationOfScientificWork),
                };
            }
        }

        public async Task<ResponseModel> UpdateClassificationOfScientificWorkAsync(Guid id, ClassificationOfScientificWorkManageModel classificationOfScientificWorkManageModel)
        {
            var classificationOfScientificWork = await _repository.GetByIdAsync(id);
            if (classificationOfScientificWork == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This classificationOfScientificWork is not exist"
                };
            }
            else
            {
                var existedClassificationOfScientificWorkName = await _repository.FetchFirstAsync(x => x.Name == classificationOfScientificWorkManageModel.Name && x.Id != id);
                if (existedClassificationOfScientificWorkName != null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "ClassificationOfScientificWork " + classificationOfScientificWorkManageModel.Name + " is exist on system. Please try again!",
                    };
                }
                else
                {
                    classificationOfScientificWorkManageModel.GetClassificationOfScientificWorkFromModel(classificationOfScientificWork);
                    return await _repository.UpdateAsync(classificationOfScientificWork);
                }
            }
        }

        public async Task<ResponseModel> DeleteClassificationOfScientificWorkAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ResponseModel> GetOtherScientificWorkByClassificationOfScientificWorkIdAsync(Guid? id)
        {
            var classificationOfScientificWork = await GetAll()
                .Include(x => x.OtherScientificWorks)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (classificationOfScientificWork.OtherScientificWorks == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This classificationOfScientificWork has no OtherScientificWork"
                };
            }
            else
            {
                List<OtherScientificWorkViewModel> otherScientificWorks = classificationOfScientificWork.OtherScientificWorks.Select(x => new OtherScientificWorkViewModel(x)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = otherScientificWorks
                };
            }
        }

    }
}
