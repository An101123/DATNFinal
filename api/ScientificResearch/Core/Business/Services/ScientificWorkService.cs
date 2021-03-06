﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Business.Models.ScientificWorks;
using ScientificResearch.Core.Business.Reflections;
using ScientificResearch.Core.Common.Constants;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Core.Entities;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Services
{
    public interface IScientificWorkService
    {
        Task<PagedList<ScientificWorkViewModel>> ListScientificWorkAsync(RequestListViewModel requestListViewModel);

        Task<ScientificWorkViewModel> GetScientificWorkByIdAsync(Guid? id);

        Task<ResponseModel> CreateScientificWorkAsync(ScientificWorkManageModel scientificWorkManagerModel);

        Task<ResponseModel> UpdateScientificWorkAsync(Guid id, ScientificWorkManageModel scientificWorkManagerModel);

        Task<ResponseModel> DeleteScientificWorkAsync(Guid id);

        Task<ResponseModel> GetLecturerByScientificWorkIdAsync(Guid? id);
    }
    public class ScientificWorkService : IScientificWorkService
    {
        private readonly IRepository<ScientificWork> _scientificWorkResponstory;
        private readonly IRepository<LecturerInScientificWork> _lecturerInScientificWorkRepository;
        private readonly IRepository<Level> _levelRepository;
        private readonly IRepository<Lecturer> _lecturerRepository;
        private readonly IMapper _mapper;

        public ScientificWorkService(IRepository<ScientificWork> scientificWorkResponstory, IRepository<LecturerInScientificWork> lecturerInScientificWorkRepository, IRepository<Level> levelRepository, IRepository<Lecturer> lecturerRepository, IMapper mapper)
        {
            _scientificWorkResponstory = scientificWorkResponstory;
            _lecturerInScientificWorkRepository = lecturerInScientificWorkRepository;
            _levelRepository = levelRepository;
            _lecturerRepository = lecturerRepository;
            _mapper = mapper;
        }

        #region private method
        private IQueryable<ScientificWork> GetAll()
        {
            return _scientificWorkResponstory.GetAll().Include(x => x.Level)
                .Include(x => x.LecturerInScientificWorks).ThenInclude(x => x.Lecturer);
        }

        private List<string> GetAllPropertyNameOfScientificWorkViewModel()
        {
            var scientificWorkViewModel = new ScientificWorkViewModel();

            var type = scientificWorkViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
        #endregion
        public async Task<PagedList<ScientificWorkViewModel>> ListScientificWorkAsync(RequestListViewModel requestListViewModel)
        {
            var list = await GetAll()
            .Where(x => (!requestListViewModel.IsActive.HasValue || x.RecordActive == requestListViewModel.IsActive)
            && (string.IsNullOrEmpty(requestListViewModel.Query)
                || (x.Name.Contains(requestListViewModel.Query)
                || (x.Content.Contains(requestListViewModel.Query))
                || (x.Level.Name.Contains(requestListViewModel.Query))
                || (x.LecturerInScientificWorks.FirstOrDefault().Lecturer.Name.Contains(requestListViewModel.Query))
                   )))
            .Select(x => new ScientificWorkViewModel(x)).ToListAsync();

            var scientificWorkViewModelProperties = GetAllPropertyNameOfScientificWorkViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(requestListViewModel.SortName) ? requestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var scientificWorkViewModelProperty in scientificWorkViewModelProperties)
            {
                var lowerTypeViewModelProperty = scientificWorkViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = scientificWorkViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(ScientificWorkViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = requestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<ScientificWorkViewModel>(list, requestListViewModel.Offset ?? CommonConstants.Config.DEFAULT_SKIP, requestListViewModel.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<ScientificWorkViewModel> GetScientificWorkByIdAsync(Guid? id)
        {
            var scientificWork = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            return new ScientificWorkViewModel(scientificWork);
        }

        public async Task<ResponseModel> DeleteScientificWorkAsync(Guid id)
        {
            var scientificWork = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (scientificWork == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This Scientific Work is not exist. Please try again!"
                };
            }
            else
            {
                await _lecturerInScientificWorkRepository.DeleteAsync(scientificWork.LecturerInScientificWorks);

                return await _scientificWorkResponstory.DeleteAsync(id);
            }
        }

        public async Task<ResponseModel> CreateScientificWorkAsync(ScientificWorkManageModel scientificWorkManageModel)
        {
            var scientificWork = await _scientificWorkResponstory.FetchFirstAsync(x => x.Name == scientificWorkManageModel.Name && x.LevelId == scientificWorkManageModel.LevelId);
            if (scientificWork != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "This ScientificWork is exist. Can you try again with the update!"
                };
            }
            else
            {
                scientificWork = _mapper.Map<ScientificWork>(scientificWorkManageModel);
                var level = await _levelRepository.GetByIdAsync(scientificWorkManageModel.LevelId);
                scientificWork.Level = level;

                await _scientificWorkResponstory.InsertAsync(scientificWork);

                var lecturerInScientificWorks = new List<LecturerInScientificWork>();
                foreach (var lecturerId in scientificWorkManageModel.LecturerIds)
                {
                    lecturerInScientificWorks.Add(new LecturerInScientificWork()
                    {
                        ScientificWorkId = scientificWork.Id,
                        LecturerId = lecturerId
                    });
                }
                _lecturerInScientificWorkRepository.GetDbContext().LecturerInScientificWorks.AddRange(lecturerInScientificWorks);
                await _lecturerInScientificWorkRepository.GetDbContext().SaveChangesAsync();

                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new ScientificWorkViewModel(scientificWork)
                };
            }
        }
        public async Task<ResponseModel> UpdateScientificWorkAsync(Guid id, ScientificWorkManageModel scientificWorkManageModel)
        {
            var scientificWork = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (scientificWork == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This ScientificWork is not exist. Please try again!"
                };
            }
            else
            {
                    await _lecturerInScientificWorkRepository.DeleteAsync(scientificWork.LecturerInScientificWorks);

                    var lecturerInScientificWorks = new List<LecturerInScientificWork>();
                    foreach (var lecturerId in scientificWorkManageModel.LecturerIds)
                    {
                        lecturerInScientificWorks.Add(new LecturerInScientificWork()
                        {
                            ScientificWorkId = scientificWork.Id,
                            LecturerId = lecturerId
                        });
                    }

                    _lecturerInScientificWorkRepository.GetDbContext().LecturerInScientificWorks.AddRange(lecturerInScientificWorks);
                    await _lecturerInScientificWorkRepository.GetDbContext().SaveChangesAsync();

                    scientificWorkManageModel.GetScientificWorkFromModel(scientificWork);
                    await _scientificWorkResponstory.UpdateAsync(scientificWork);

                    scientificWork = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
                    return new ResponseModel
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Data = new ScientificWorkViewModel(scientificWork)
                    };
            }

        }

        public async Task<ResponseModel> GetLecturerByScientificWorkIdAsync(Guid? id)
        {
            var scientificWork = await GetAll().FirstAsync(x => x.Id == id);
            List<LecturerViewModel> lecturers = scientificWork.LecturerInScientificWorks.Select(x => new LecturerViewModel(x.Lecturer)).ToList();
            return new ResponseModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = lecturers
            };
        }

    }
}
