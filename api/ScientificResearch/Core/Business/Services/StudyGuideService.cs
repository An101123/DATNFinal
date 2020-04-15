using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.StudyGuides;
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
    public interface IStudyGuideService
    {
        Task<PagedList<StudyGuideViewModel>> ListStudyGuideAsync(RequestListViewModel requestListViewModel);

        Task<StudyGuideViewModel> GetStudyGuideByIdAsync(Guid? id);

        Task<ResponseModel> CreateStudyGuideAsync(StudyGuideManageModel studyGuideManagerModel);

        Task<ResponseModel> UpdateStudyGuideAsync(Guid id, StudyGuideManageModel studyGuideManagerModel);

        Task<ResponseModel> DeleteStudyGuideAsync(Guid id);
    }
    public class StudyGuideService : IStudyGuideService
    {
        private readonly IRepository<StudyGuide> _studyGuideResponstory;
        private readonly IRepository<LevelStudyGuide> _levelStudyGuideRepository;
        private readonly IRepository<Lecturer> _lecturerRepository;
        private readonly IMapper _mapper;

        public StudyGuideService(IRepository<StudyGuide> studyGuideResponstory, IRepository<LevelStudyGuide> levelStudyGuideRepository, IRepository<Lecturer> lecturerRepository, IMapper mapper)
        {
            _studyGuideResponstory = studyGuideResponstory;
            _levelStudyGuideRepository = levelStudyGuideRepository;
            _lecturerRepository = lecturerRepository;
            _mapper = mapper;
        }

        #region private method
        private IQueryable<StudyGuide> GetAll()
        {
            return _studyGuideResponstory.GetAll().Include(x => x.LevelStudyGuide)
                .Include(x => x.Lecturer);
        }

        private List<string> GetAllPropertyNameOfStudyGuideViewModel()
        {
            var studyGuideViewModel = new StudyGuideViewModel();

            var type = studyGuideViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
        #endregion
        public async Task<PagedList<StudyGuideViewModel>> ListStudyGuideAsync(RequestListViewModel requestListViewModel)
        {
            var list = await GetAll()
            .Where(x => (!requestListViewModel.IsActive.HasValue || x.RecordActive == requestListViewModel.IsActive)
            && (string.IsNullOrEmpty(requestListViewModel.Query)
                || (x.Name.Contains(requestListViewModel.Query)
                || (x.LevelStudyGuide.Name.Contains(requestListViewModel.Query))
                || (x.Literacy.Contains(requestListViewModel.Query))
                || (x.PlaceOfTraining.Contains(requestListViewModel.Query))
                || (x.Lecturer.Name.Contains(requestListViewModel.Query))
                )))
            .Select(x => new StudyGuideViewModel(x)).ToListAsync();

            var studyGuideViewModelProperties = GetAllPropertyNameOfStudyGuideViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(requestListViewModel.SortName) ? requestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var studyGuideViewModelProperty in studyGuideViewModelProperties)
            {
                var lowerTypeViewModelProperty = studyGuideViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = studyGuideViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(StudyGuideViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = requestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<StudyGuideViewModel>(list, requestListViewModel.Offset ?? CommonConstants.Config.DEFAULT_SKIP, requestListViewModel.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<StudyGuideViewModel> GetStudyGuideByIdAsync(Guid? id)
        {
            var studyGuide = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            return new StudyGuideViewModel(studyGuide);
        }

        public async Task<ResponseModel> DeleteStudyGuideAsync(Guid id)
        {
            return await _studyGuideResponstory.DeleteAsync(id);
        }

        public async Task<ResponseModel> CreateStudyGuideAsync(StudyGuideManageModel studyGuideManageModel)
        {
            var studyGuide = await _studyGuideResponstory.FetchFirstAsync(x => x.Name == studyGuideManageModel.Name && x.LevelStudyGuideId == studyGuideManageModel.LevelStudyGuideId);
            if (studyGuide != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "This StudyGuide is exist. Can you try again with the update!"
                };
            }
            else
            {
                var levelStudyGuide = await _levelStudyGuideRepository.GetByIdAsync(studyGuideManageModel.LevelStudyGuideId);
                var lecturer = await _lecturerRepository.GetByIdAsync(studyGuideManageModel.LecturerId);
                studyGuide = _mapper.Map<StudyGuide>(studyGuideManageModel);
                studyGuide.LevelStudyGuide = levelStudyGuide;
                studyGuide.Lecturer = lecturer;

                await _studyGuideResponstory.InsertAsync(studyGuide);
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new StudyGuideViewModel(studyGuide)
                };
            }
        }
        public async Task<ResponseModel> UpdateStudyGuideAsync(Guid id, StudyGuideManageModel studyGuideManageModel)
        {
            var studyGuide = await _studyGuideResponstory.GetByIdAsync(id);
            if (studyGuide == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This StudyGuide is not exist. Please try again!"
                };
            }
            else
            {
                var existedStudyGuide = await _studyGuideResponstory.FetchFirstAsync(x => x.Name == studyGuideManageModel.Name && x.LevelStudyGuideId == studyGuideManageModel.LevelStudyGuideId && x.Id != id);
                if (existedStudyGuide != null)
                {
                    var levelStudyGuide = await _studyGuideResponstory.GetByIdAsync(studyGuideManageModel.LevelStudyGuideId);
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "StudyGuide " + existedStudyGuide.Name + " already created on LevelStudyGuide " + levelStudyGuide.Name,
                    };
                }
                else
                {
                    studyGuideManageModel.GetStudyGuideFromModel(studyGuide);
                    return await _studyGuideResponstory.UpdateAsync(studyGuide);
                }
            }

        }

    }
}
