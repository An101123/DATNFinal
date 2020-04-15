using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.OtherScientificWorks;
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
   
    public interface IOtherScientificWorkService
    {
        Task<PagedList<OtherScientificWorkViewModel>> ListOtherScientificWorkAsync(RequestListViewModel requestListViewModel);

        Task<OtherScientificWorkViewModel> GetOtherScientificWorkByIdAsync(Guid? id);

        Task<ResponseModel> CreateOtherScientificWorkAsync(OtherScientificWorkManageModel otherScientificWorkManagerModel);

        Task<ResponseModel> UpdateOtherScientificWorkAsync(Guid id, OtherScientificWorkManageModel otherScientificWorkManagerModel);

        Task<ResponseModel> DeleteOtherScientificWorkAsync(Guid id);
    }
    public class OtherScientificWorkService : IOtherScientificWorkService
    {
        private readonly IRepository<OtherScientificWork> _otherScientificWorkResponstory;
        private readonly IRepository<ClassificationOfScientificWork> _classificationOfScientificWorkRepository;
        private readonly IRepository<Lecturer> _lecturerRepository;
        private readonly IMapper _mapper;

        public OtherScientificWorkService(IRepository<OtherScientificWork> otherScientificWorkResponstory, IRepository<ClassificationOfScientificWork> classificationOfScientificWorkRepository, IRepository<Lecturer> lecturerRepository, IMapper mapper)
        {
            _otherScientificWorkResponstory = otherScientificWorkResponstory;
            _classificationOfScientificWorkRepository = classificationOfScientificWorkRepository;
            _lecturerRepository = lecturerRepository;
            _mapper = mapper;
        }

        #region private method
        private IQueryable<OtherScientificWork> GetAll()
        {
            return _otherScientificWorkResponstory.GetAll().Include(x => x.ClassificationOfScientificWork)
                .Include(x => x.Lecturer);
        }

        private List<string> GetAllPropertyNameOfOtherScientificWorkViewModel()
        {
            var otherScientificWorkViewModel = new OtherScientificWorkViewModel();

            var type = otherScientificWorkViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
        #endregion
        public async Task<PagedList<OtherScientificWorkViewModel>> ListOtherScientificWorkAsync(RequestListViewModel requestListViewModel)
        {
            var list = await GetAll()
            .Where(x => (!requestListViewModel.IsActive.HasValue || x.RecordActive == requestListViewModel.IsActive)
            && (string.IsNullOrEmpty(requestListViewModel.Query)
                || (x.Name.Contains(requestListViewModel.Query)
                || (x.ClassificationOfScientificWork.Name.Contains(requestListViewModel.Query))
                || (x.Lecturer.Name.Contains(requestListViewModel.Query))
                )))
            .Select(x => new OtherScientificWorkViewModel(x)).ToListAsync();

            var otherScientificWorkViewModelProperties = GetAllPropertyNameOfOtherScientificWorkViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(requestListViewModel.SortName) ? requestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var otherScientificWorkViewModelProperty in otherScientificWorkViewModelProperties)
            {
                var lowerTypeViewModelProperty = otherScientificWorkViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = otherScientificWorkViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(OtherScientificWorkViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = requestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<OtherScientificWorkViewModel>(list, requestListViewModel.Offset ?? CommonConstants.Config.DEFAULT_SKIP, requestListViewModel.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<OtherScientificWorkViewModel> GetOtherScientificWorkByIdAsync(Guid? id)
        {
            var otherScientificWork = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            return new OtherScientificWorkViewModel(otherScientificWork);
        }

        public async Task<ResponseModel> DeleteOtherScientificWorkAsync(Guid id)
        {
            return await _otherScientificWorkResponstory.DeleteAsync(id);
        }

        public async Task<ResponseModel> CreateOtherScientificWorkAsync(OtherScientificWorkManageModel otherScientificWorkManageModel)
        {
            var otherScientificWork = await _otherScientificWorkResponstory.FetchFirstAsync(x => x.Name == otherScientificWorkManageModel.Name && x.ClassificationOfScientificWorkId == otherScientificWorkManageModel.ClassificationOfScientificWorkId);
            if (otherScientificWork != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "This OtherScientificWork is exist. Can you try again with the update!"
                };
            }
            else
            {
                var classificationOfScientificWork = await _classificationOfScientificWorkRepository.GetByIdAsync(otherScientificWorkManageModel.ClassificationOfScientificWorkId);
                var lecturer = await _lecturerRepository.GetByIdAsync(otherScientificWorkManageModel.LecturerId);
                otherScientificWork = _mapper.Map<OtherScientificWork>(otherScientificWorkManageModel);
                otherScientificWork.ClassificationOfScientificWork = classificationOfScientificWork;
                otherScientificWork.Lecturer = lecturer;

                await _otherScientificWorkResponstory.InsertAsync(otherScientificWork);
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new OtherScientificWorkViewModel(otherScientificWork)
                };
            }
        }
        public async Task<ResponseModel> UpdateOtherScientificWorkAsync(Guid id, OtherScientificWorkManageModel otherScientificWorkManageModel)
        {
            var otherScientificWork = await _otherScientificWorkResponstory.GetByIdAsync(id);
            if (otherScientificWork == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This OtherScientificWork is not exist. Please try again!"
                };
            }
            else
            {
                var existedOtherScientificWork = await _otherScientificWorkResponstory.FetchFirstAsync(x => x.Name == otherScientificWorkManageModel.Name && x.ClassificationOfScientificWorkId == otherScientificWorkManageModel.ClassificationOfScientificWorkId && x.Id != id);
                if (existedOtherScientificWork != null)
                {
                    var classificationOfScientificWork = await _otherScientificWorkResponstory.GetByIdAsync(otherScientificWorkManageModel.ClassificationOfScientificWorkId);
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "OtherScientificWork " + existedOtherScientificWork.Name + " already created on ClassificationOfScientificWork " + classificationOfScientificWork.Name,
                    };
                }
                else
                {
                    otherScientificWorkManageModel.GetOtherScientificWorkFromModel(otherScientificWork);
                    return await _otherScientificWorkResponstory.UpdateAsync(otherScientificWork);
                }
            }

        }

    }
}
