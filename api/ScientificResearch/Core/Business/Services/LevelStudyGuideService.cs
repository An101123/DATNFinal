using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.LevelStudyGuides;
using ScientificResearch.Core.Business.Models.StudyGuides;
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
 
    public interface ILevelStudyGuideService
    {
        Task<List<LevelStudyGuideViewModel>> GetAllLevelStudyGuide();
        Task<PagedList<LevelStudyGuideViewModel>> ListLevelStudyGuideAsync(RequestListViewModel requestListViewModel);
        Task<LevelStudyGuide> GetLevelStudyGuideByIdAsync(Guid? id);
        Task<ResponseModel> CreateLevelStudyGuideAsync(LevelStudyGuideManageModel levelStudyGuideManagerModel);
        Task<ResponseModel> UpdateLevelStudyGuideAsync(Guid id, LevelStudyGuideManageModel levelStudyGuideManagerModel);
        Task<ResponseModel> DeleteLevelStudyGuideAsync(Guid id);
        Task<ResponseModel> GetStudyGuideByLevelStudyGuideIdAsync(Guid? id);
    }
    public class LevelStudyGuideService : ILevelStudyGuideService
    {
        private readonly IRepository<LevelStudyGuide> _repository;
        private readonly IMapper _mapper;

        public LevelStudyGuideService(IRepository<LevelStudyGuide> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region private method


        private IQueryable<LevelStudyGuide> GetAll()
        {
            return _repository.GetAll().Where(i => !i.RecordDeleted);
        }

        private List<string> GetAllPropertyNameOfLevelStudyGuideViewModel()
        {
            var levelStudyGuideViewModel = new LevelStudyGuideViewModel();

            var type = levelStudyGuideViewModel.GetType();



            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }

        #endregion
        public async Task<List<LevelStudyGuideViewModel>> GetAllLevelStudyGuide()
        {
            var list = await GetAll().Select(x => new LevelStudyGuideViewModel(x)).ToListAsync();
            return list;
        }

        public async Task<PagedList<LevelStudyGuideViewModel>> ListLevelStudyGuideAsync(RequestListViewModel requestListViewModel)
        {
            var list = await GetAll()
                .Where(x => (!requestListViewModel.IsActive.HasValue || x.RecordActive == requestListViewModel.IsActive)
                && (string.IsNullOrEmpty(requestListViewModel.Query)
                    || (x.Name.Contains(requestListViewModel.Query)
                    )))
                .Select(x => new LevelStudyGuideViewModel(x)).ToListAsync();

            var levelStudyGuideViewModelProperties = GetAllPropertyNameOfLevelStudyGuideViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(requestListViewModel.SortName) ? requestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var levelStudyGuideViewModelProperty in levelStudyGuideViewModelProperties)
            {
                var lowerTypeViewModelProperty = levelStudyGuideViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = levelStudyGuideViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(LevelStudyGuideViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = requestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<LevelStudyGuideViewModel>(list, requestListViewModel.Offset ?? CommonConstants.Config.DEFAULT_SKIP, requestListViewModel.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<LevelStudyGuide> GetLevelStudyGuideByIdAsync(Guid? id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResponseModel> CreateLevelStudyGuideAsync(LevelStudyGuideManageModel levelStudyGuideManageModel)
        {
            var levelStudyGuide = await _repository.FetchFirstAsync(x => x.Name == levelStudyGuideManageModel.Name);
            if (levelStudyGuide != null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "This LevelStudyGuide is exist"
                };
            }
            else
            {
                levelStudyGuide = new LevelStudyGuide();
                levelStudyGuideManageModel.GetLevelStudyGuideFromModel(levelStudyGuide);
                await _repository.InsertAsync(levelStudyGuide);
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new LevelStudyGuideViewModel(levelStudyGuide),
                };
            }
        }

        public async Task<ResponseModel> UpdateLevelStudyGuideAsync(Guid id, LevelStudyGuideManageModel levelStudyGuideManageModel)
        {
            var levelStudyGuide = await _repository.GetByIdAsync(id);
            if (levelStudyGuide == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This levelStudyGuide is not exist"
                };
            }
            else
            {
                var existedLevelStudyGuideName = await _repository.FetchFirstAsync(x => x.Name == levelStudyGuideManageModel.Name && x.Id != id);
                if (existedLevelStudyGuideName != null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "LevelStudyGuide " + levelStudyGuideManageModel.Name + " is exist on system. Please try again!",
                    };
                }
                else
                {
                    levelStudyGuideManageModel.GetLevelStudyGuideFromModel(levelStudyGuide);
                    return await _repository.UpdateAsync(levelStudyGuide);
                }
            }
        }

        public async Task<ResponseModel> DeleteLevelStudyGuideAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ResponseModel> GetStudyGuideByLevelStudyGuideIdAsync(Guid? id)
        {
            var levelStudyGuide = await GetAll()
                .Include(x => x.StudyGuides)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (levelStudyGuide.StudyGuides == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This levelStudyGuide has no ScientificWork"
                };
            }
            else
            {
                List<StudyGuideViewModel> scientificWorks = levelStudyGuide.StudyGuides.Select(x => new StudyGuideViewModel(x)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = scientificWorks
                };
            }
        }

    }
}
