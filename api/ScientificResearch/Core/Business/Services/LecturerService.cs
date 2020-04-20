﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Business.Models.Base;
using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Business.Models.OtherScientificWorks;
using ScientificResearch.Core.Business.Models.PublishBooks;
using ScientificResearch.Core.Business.Models.ScientificReports;
using ScientificResearch.Core.Business.Models.ScientificWorks;
using ScientificResearch.Core.Business.Models.StudyGuides;
using ScientificResearch.Core.Business.Reflections;
using ScientificResearch.Core.Common.Constants;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Services
{
    public interface ILecturerService
    {
        Task<List<LecturerViewModel>> GetAllLecturer();

        Task<PagedList<LecturerViewModel>> ListLecturerAsync(RequestListViewModel requestListViewModel);
        Task<LecturerViewModel> GetLecturerByIdAsync(Guid? id);
        Task<ResponseModel> CreateLecturerAsync(LecturerManageModel lecturerManageModel);
        Task<ResponseModel> UpdateLecturerAsync(Guid id, LecturerManageModel lecturerManageModel);
        Task<ResponseModel> DeleteLecturerAsync(Guid id);
        Task<ResponseModel> GetScientificWorkByLecturerIdAsync(Guid? id);
        Task<ResponseModel> GetScientificReportByLecturerIdAsync(Guid? id);
        Task<ResponseModel> GetPublishBookByLecturerIdAsync(Guid? id);
        Task<ResponseModel> GetStudyGuideByLecturerIdAsync(Guid? id);
        Task<ResponseModel> GetOtherScientificWorkByLecturerIdAsync(Guid? id);

    }

    public class LecturerService : ILecturerService
    {
        private readonly IRepository<Lecturer> _lecturerRepository;
        private readonly IMapper _mapper;

        #region constructor

        public LecturerService(IRepository<Lecturer> lecturerRepository, IMapper mapper)
        {
            _lecturerRepository = lecturerRepository;
            _mapper = mapper;
        }

        #endregion

        #region private method

        private IQueryable<Lecturer> GetAll()
        {
            return _lecturerRepository.GetAll()
                .Include(x => x.ScientificReports)
                    .ThenInclude(x => x.ScientificReportType)
                .Include(x => x.ScientificWorks)
                    .ThenInclude(x => x.Level)
                    .Include(x => x.PublishBooks).ThenInclude(x=>x.BookCategory)
                    .Include(x=>x.StudyGuides).ThenInclude(x=>x.LevelStudyGuide)
                    .Include(x => x.OtherScientificWorks).ThenInclude(x => x.ClassificationOfScientificWork);
        }

        private List<string> GetAllPropertyNameOfLecturerViewModel()
        {
            var lecturerViewModel = new LecturerViewModel();

            var type = lecturerViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }

        private async Task ScoreCount()
        {
            var list = await GetAll().ToListAsync();
            foreach (var lecturer in list)
            {
                float sum = 0;
                if (lecturer.ScientificReports.Count > 0)
                {
                    foreach (var scientificReport in lecturer.ScientificReports)
                    {
                        sum += scientificReport.ScientificReportType.Score;
                    }
                }

                if (lecturer.ScientificWorks.Count > 0)
                {
                    foreach (var scientificWork in lecturer.ScientificWorks)
                    {
                        sum += scientificWork.Level.Score;
                    }
                }
                if (lecturer.PublishBooks.Count > 0)
                {
                    foreach (var publishBook in lecturer.PublishBooks)
                    {
                        sum += publishBook.BookCategory.Score;
                    }
                }
                if (lecturer.StudyGuides.Count > 0)
                {
                    foreach (var studyGuide in lecturer.StudyGuides)
                    {
                        sum += studyGuide.LevelStudyGuide.Score;
                    }
                }
                if (lecturer.OtherScientificWorks.Count > 0)
                {
                    foreach (var otherScientificWork in lecturer.OtherScientificWorks)
                    {
                        sum += otherScientificWork.ClassificationOfScientificWork.Score;
                    }
                }
                lecturer.Total = sum;
                await _lecturerRepository.UpdateAsync(lecturer);
            }
        }

        private async Task TotalHours()
        {
            var list = await GetAll().ToListAsync();
            foreach (var lecturer in list)
            {
                int hour = 0;
                if (lecturer.ScientificReports.Count > 0)
                {
                    foreach (var scientificReport in lecturer.ScientificReports)
                    {
                        hour += scientificReport.ScientificReportType.HoursConverted;
                    }
                }

                if (lecturer.ScientificWorks.Count > 0)
                {
                    foreach (var scientificWork in lecturer.ScientificWorks)
                    {
                        hour += scientificWork.Level.HoursConverted;
                    }
                }
                if (lecturer.PublishBooks.Count > 0)
                {
                    foreach (var publishBook in lecturer.PublishBooks)
                    {
                        hour += publishBook.BookCategory.HoursConverted;
                    }
                }
                if (lecturer.StudyGuides.Count > 0)
                {
                    foreach (var studyGuide in lecturer.StudyGuides)
                    {
                        hour += studyGuide.LevelStudyGuide.HoursConverted;
                    }
                }
                if (lecturer.OtherScientificWorks.Count > 0)
                {
                    foreach (var otherScientificWork in lecturer.OtherScientificWorks)
                    {
                      hour += otherScientificWork.ClassificationOfScientificWork.HoursConverted;
                    }
                }
                lecturer.TotalHour = hour;
                await _lecturerRepository.UpdateAsync(lecturer);
            }
        }
        #endregion

        public async Task<List<LecturerViewModel>> GetAllLecturer()
        {
            var list = await GetAll().Select(x => new LecturerViewModel(x)).ToListAsync();
            return list;
        }

        public async Task<PagedList<LecturerViewModel>> ListLecturerAsync(RequestListViewModel requestListViewModel)
        {
            await ScoreCount();
            await TotalHours();
            var list = await GetAll()
                .Where(x => (!requestListViewModel.IsActive.HasValue || x.RecordActive == requestListViewModel.IsActive)
                && (string.IsNullOrEmpty(requestListViewModel.Query)
                    || (x.Name.Contains(requestListViewModel.Query)
                    )))
                .Select(x => new LecturerViewModel(x)).ToListAsync();

            var lecturerViewModelProperties = GetAllPropertyNameOfLecturerViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(requestListViewModel.SortName) ? requestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = lecturerViewModelProperties.FirstOrDefault(x => x == requestPropertyName);

            //foreach (var tableViewModelProperty in tableViewModelProperties)
            //{
            //    var lowerTypeViewModelProperty = tableViewModelProperty.ToLower();
            //    if (lowerTypeViewModelProperty.Equals(requestPropertyName))
            //    {
            //        matchedPropertyName = tableViewModelProperty;
            //        break;
            //    }
            //}

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Total";
            }

            var type = typeof(LecturerViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = requestListViewModel.IsDesc ? list.OrderBy(x => sortProperty.GetValue(x, null)).ToList() : list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<LecturerViewModel>(list, requestListViewModel.Offset ?? CommonConstants.Config.DEFAULT_SKIP, requestListViewModel.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<LecturerViewModel> GetLecturerByIdAsync(Guid? id)
        {
            var table = await GetAll().FirstAsync(x => x.Id == id);
            return new LecturerViewModel(table);
        }

        //public async Task<ResponseModel> CreateLecturerAsync(LecturerManageModel lecturerManageModel)
        //{
        //    var lecturer = _mapper.Map<Lecturer>(lecturerManageModel);
        //    await _lecturerRepository.InsertAsync(lecturer);
        //    return new ResponseModel
        //    {
        //        StatusCode = System.Net.HttpStatusCode.OK,
        //        Data = new LecturerViewModel(lecturer)
        //    };
        //}
        public async Task<ResponseModel> CreateLecturerAsync(LecturerManageModel lecturerManageModel)
        {
            var lecturer = new Lecturer();
            lecturerManageModel.GetLecturerFromModel(lecturer);
            await _lecturerRepository.InsertAsync(lecturer);
            return new ResponseModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new LecturerViewModel(lecturer),
            };
        }

        public async Task<ResponseModel> UpdateLecturerAsync(Guid id, LecturerManageModel lecturerManageModel)
        {
            var lecturer = await _lecturerRepository.GetByIdAsync(id);
            if (lecturer == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer is not exist"
                };
            }
            else
            {
                lecturerManageModel.GetLecturerFromModel(lecturer);
                return await _lecturerRepository.UpdateAsync(lecturer);
            }
        }

        public async Task<ResponseModel> DeleteLecturerAsync(Guid id)
        {
            return await _lecturerRepository.DeleteAsync(id);
        }

        public async Task<ResponseModel> GetScientificWorkByLecturerIdAsync(Guid? id)
        {
            var lecturer = await GetAll().FirstAsync(x => x.Id == id);
            if (lecturer.ScientificWorks == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no scientific work"
                };
            }
            else
            {
                List<ScientificWorkViewModel> scientificWorks = lecturer.ScientificWorks.Select(x => new ScientificWorkViewModel(x)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = scientificWorks
                };
            }
        }

        public async Task<ResponseModel> GetScientificReportByLecturerIdAsync(Guid? id)
        {
            var lecturer = await GetAll().FirstAsync(x => x.Id == id);
            if (lecturer.ScientificReports == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no scientific report"
                };
            }
            else
            {
                List<ScientificReportViewModel> scientificReports = lecturer.ScientificReports.Select(x => new ScientificReportViewModel(x)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = scientificReports
                };
            }
        }

        public async Task<ResponseModel> GetPublishBookByLecturerIdAsync(Guid? id)
        {
            var lecturer = await GetAll().FirstAsync(x => x.Id == id);
            if (lecturer.PublishBooks == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no publishBook"
                };
            }
            else
            {
                List<PublishBookViewModel> publishBooks = lecturer.PublishBooks.Select(x => new PublishBookViewModel(x)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = publishBooks
                };
            }
        }
        public async Task<ResponseModel> GetStudyGuideByLecturerIdAsync(Guid? id)
        {
            var lecturer = await GetAll().FirstAsync(x => x.Id == id);
            if (lecturer.StudyGuides == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no studyGuide"
                };
            }
            else
            {
                List<StudyGuideViewModel> studyGuides = lecturer.StudyGuides.Select(x => new StudyGuideViewModel(x)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = studyGuides
                };
            }
        }
        public async Task<ResponseModel> GetOtherScientificWorkByLecturerIdAsync(Guid? id)
        {
            var lecturer = await GetAll().FirstAsync(x => x.Id == id);
            if (lecturer.OtherScientificWorks == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no OtherScientificWork"
                };
            }
            else
            {
                List<OtherScientificWorkViewModel> otherScientificWorks = lecturer.OtherScientificWorks.Select(x => new OtherScientificWorkViewModel(x)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = otherScientificWorks
                };
            }
        }

    }
    

}
