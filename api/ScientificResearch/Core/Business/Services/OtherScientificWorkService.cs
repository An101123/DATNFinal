using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.Lecturers;
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

        Task<ResponseModel> GetLecturerByOtherScientificWorkIdAsync(Guid? id);
    }
    public class OtherScientificWorkService : IOtherScientificWorkService
    {
        private readonly IRepository<OtherScientificWork> _otherScientificWorkResponstory;
        private readonly IRepository<LecturerInOtherScientificWork> _lecturerInOtherScientificWorkRepository;
        private readonly IRepository<ClassificationOfScientificWork> _classificationOfScientificWorkRepository;
        private readonly IRepository<Lecturer> _lecturerRepository;
        private readonly IMapper _mapper;

        public OtherScientificWorkService(IRepository<OtherScientificWork> otherScientificWorkResponstory, IRepository<LecturerInOtherScientificWork> lecturerInOtherScientificWorkRepository, IRepository<ClassificationOfScientificWork> classificationOfScientificWorkRepository, IRepository<Lecturer> lecturerRepository, IMapper mapper)
        {
            _otherScientificWorkResponstory = otherScientificWorkResponstory;
            _lecturerInOtherScientificWorkRepository = lecturerInOtherScientificWorkRepository;
            _classificationOfScientificWorkRepository = classificationOfScientificWorkRepository;
            _lecturerRepository = lecturerRepository;
            _mapper = mapper;
        }

        #region private method
        private IQueryable<OtherScientificWork> GetAll()
        {
            return _otherScientificWorkResponstory.GetAll().Include(x => x.ClassificationOfScientificWork)
                .Include(x => x.LecturerInOtherScientificWorks).ThenInclude(x => x.Lecturer);
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
                || (x.LecturerInOtherScientificWorks.FirstOrDefault().Lecturer.Name.Contains(requestListViewModel.Query))
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
            var otherScientificWork = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (otherScientificWork == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This Scientific Work is not exist. Please try again!"
                };
            }
            else
            {
                await _lecturerInOtherScientificWorkRepository.DeleteAsync(otherScientificWork.LecturerInOtherScientificWorks);

                return await _otherScientificWorkResponstory.DeleteAsync(id);
            }
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
                otherScientificWork = _mapper.Map<OtherScientificWork>(otherScientificWorkManageModel);
                var classificationOfScientificWork = await _classificationOfScientificWorkRepository.GetByIdAsync(otherScientificWorkManageModel.ClassificationOfScientificWorkId);
                otherScientificWork.ClassificationOfScientificWork = classificationOfScientificWork;

                await _otherScientificWorkResponstory.InsertAsync(otherScientificWork);

                var lecturerInOtherScientificWorks = new List<LecturerInOtherScientificWork>();
                foreach (var lecturerId in otherScientificWorkManageModel.LecturerIds)
                {
                    lecturerInOtherScientificWorks.Add(new LecturerInOtherScientificWork()
                    {
                        OtherScientificWorkId = otherScientificWork.Id,
                        LecturerId = lecturerId
                    });
                }
                _lecturerInOtherScientificWorkRepository.GetDbContext().LecturerInOtherScientificWorks.AddRange(lecturerInOtherScientificWorks);
                await _lecturerInOtherScientificWorkRepository.GetDbContext().SaveChangesAsync();

                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new OtherScientificWorkViewModel(otherScientificWork)
                };
            }
        }
        public async Task<ResponseModel> UpdateOtherScientificWorkAsync(Guid id, OtherScientificWorkManageModel otherScientificWorkManageModel)
        {
            var otherScientificWork = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
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
                await _lecturerInOtherScientificWorkRepository.DeleteAsync(otherScientificWork.LecturerInOtherScientificWorks);

                var lecturerInOtherScientificWorks = new List<LecturerInOtherScientificWork>();
                foreach (var lecturerId in otherScientificWorkManageModel.LecturerIds)
                {
                    lecturerInOtherScientificWorks.Add(new LecturerInOtherScientificWork()
                    {
                        OtherScientificWorkId = otherScientificWork.Id,
                        LecturerId = lecturerId
                    });
                }

                _lecturerInOtherScientificWorkRepository.GetDbContext().LecturerInOtherScientificWorks.AddRange(lecturerInOtherScientificWorks);
                await _lecturerInOtherScientificWorkRepository.GetDbContext().SaveChangesAsync();

                otherScientificWorkManageModel.GetOtherScientificWorkFromModel(otherScientificWork);
                await _otherScientificWorkResponstory.UpdateAsync(otherScientificWork);

                otherScientificWork = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new OtherScientificWorkViewModel(otherScientificWork)
                };
            }

        }

        public async Task<ResponseModel> GetLecturerByOtherScientificWorkIdAsync(Guid? id)
        {
            var otherScientificWork = await GetAll().FirstAsync(x => x.Id == id);
            List<LecturerViewModel> lecturers = otherScientificWork.LecturerInOtherScientificWorks.Select(x => new LecturerViewModel(x.Lecturer)).ToList();
            return new ResponseModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = lecturers
            };
        }

    }
}
