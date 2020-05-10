using AutoMapper;
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

        Task<PagedList<LecturerViewModel>> ListLecturerAsync(RequestListViewModel requestListViewModel, DateTime? startTime, DateTime? endTime);
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
                .Include(x => x.LecturerInScientificReports)
                .ThenInclude(x => x.ScientificReport)
                    .ThenInclude(x => x.ScientificReportType)
                .Include(x => x.LecturerInScientificWorks)
                    .ThenInclude(x => x.ScientificWork)
                    .ThenInclude(x => x.Level)
                    .Include(x => x.LecturerInPublishBooks)
                    .ThenInclude(x => x.PublishBook).ThenInclude(x => x.BookCategory)
                    .Include(x => x.StudyGuides).ThenInclude(x => x.LevelStudyGuide)
                    .Include(x => x.LecturerInOtherScientificWorks).ThenInclude(x => x.OtherScientificWork).ThenInclude(x => x.ClassificationOfScientificWork);
        }

        private List<string> GetAllPropertyNameOfLecturerViewModel()
        {
            var lecturerViewModel = new LecturerViewModel();

            var type = lecturerViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
        private async Task ScoreAndHourCount()
        {
            var list = await GetAll().ToListAsync();
            foreach (var lecturer in list)
            {
                float sum = 0;
                int hour = 0;
                if (lecturer.LecturerInScientificReports.Count > 0)
                {
                    foreach (var scientificReport in lecturer.LecturerInScientificReports)
                    {
                        sum += scientificReport.ScientificReport.ScientificReportType.Score;
                        hour += scientificReport.ScientificReport.ScientificReportType.HoursConverted;
                    }
                }

                if (lecturer.LecturerInScientificWorks.Count > 0)
                {
                    foreach (var scientificWork in lecturer.LecturerInScientificWorks)
                    {
                        sum += scientificWork.ScientificWork.Level.Score;
                        hour += scientificWork.ScientificWork.Level.HoursConverted;
                    }
                }
                if (lecturer.LecturerInPublishBooks.Count > 0)
                {
                    foreach (var publishBook in lecturer.LecturerInPublishBooks)
                    {
                        sum += publishBook.PublishBook.BookCategory.Score;
                        hour += publishBook.PublishBook.BookCategory.HoursConverted;
                    }
                }
                if (lecturer.StudyGuides.Count > 0)
                {
                    foreach (var studyGuide in lecturer.StudyGuides)
                    {
                        sum += studyGuide.LevelStudyGuide.Score;
                        hour += studyGuide.LevelStudyGuide.HoursConverted;
                    }
                }
                if (lecturer.LecturerInOtherScientificWorks.Count > 0)
                {
                    foreach (var otherScientificWork in lecturer.LecturerInOtherScientificWorks)
                    {
                        sum += otherScientificWork.OtherScientificWork.ClassificationOfScientificWork.Score;
                        hour += otherScientificWork.OtherScientificWork.ClassificationOfScientificWork.HoursConverted;
                    }
                }
                lecturer.Total = sum;
                lecturer.TotalHour = hour;
                await _lecturerRepository.UpdateAsync(lecturer);
            }
        }
        private async Task ScoreAndHourByStage( DateTime? startTime, DateTime? endTime)
        {
            var list = await GetAll().ToListAsync();
            foreach (var lecturer in list)
            {
                if (startTime != null && endTime != null)
                {
                    float sum = 0;
                    int hour = 0;
                    if (lecturer.LecturerInScientificReports.Count > 0)
                    {
                        foreach (var scientificReport in lecturer.LecturerInScientificReports)
                        {
                            if (scientificReport.ScientificReport.Time >= startTime && scientificReport.ScientificReport.Time <= endTime)
                            {
                                sum += scientificReport.ScientificReport.ScientificReportType.Score;
                                hour += scientificReport.ScientificReport.ScientificReportType.HoursConverted;
                            }
                        }
                    }
                    if (lecturer.LecturerInScientificWorks.Count > 0)
                    {
                        foreach (var scientificWork in lecturer.LecturerInScientificWorks)
                        {
                            if (scientificWork.ScientificWork.Time >= startTime && scientificWork.ScientificWork.Time <= endTime)
                            {
                                sum += scientificWork.ScientificWork.Level.Score;
                                hour += scientificWork.ScientificWork.Level.HoursConverted;
                            }
                        }
                    }
                    if (lecturer.LecturerInPublishBooks.Count > 0)
                    {
                        foreach (var publishBook in lecturer.LecturerInPublishBooks)
                        {
                            if (publishBook.PublishBook.Time >= startTime && publishBook.PublishBook.Time <= endTime)
                            {
                                sum += publishBook.PublishBook.BookCategory.Score;
                                hour += publishBook.PublishBook.BookCategory.HoursConverted;
                            }
                        }
                    }
                    if (lecturer.LecturerInOtherScientificWorks.Count > 0)
                    {
                        foreach (var otherScientificWork in lecturer.LecturerInOtherScientificWorks)
                        {
                            if (otherScientificWork.OtherScientificWork.Time >= startTime && otherScientificWork.OtherScientificWork.Time <= endTime)
                            {
                                sum += otherScientificWork.OtherScientificWork.ClassificationOfScientificWork.Score;
                                hour += otherScientificWork.OtherScientificWork.ClassificationOfScientificWork.HoursConverted;
                            }
                        }
                    }
                    if (lecturer.StudyGuides.Count > 0)
                    {
                        foreach (var studyGuide in lecturer.StudyGuides)
                        {
                            if (studyGuide.InstructionTime >= startTime && studyGuide.InstructionTime <= endTime)
                            {
                                sum += studyGuide.LevelStudyGuide.Score;
                                hour += studyGuide.LevelStudyGuide.HoursConverted;
                            }
                        }
                    }

                    lecturer.Total = sum;
                    lecturer.TotalHour = hour;
                    await _lecturerRepository.UpdateAsync(lecturer);
                }
            }
        }
        //private async Task HourByStage(DateTime? startTime, DateTime? endTime)
        //{
        //    var list = await GetAll().ToListAsync();
        //    foreach (var lecturer in list)
        //    {
        //        if (startTime != null && endTime != null)
        //        {
        //            float sum = 0;
        //            if (lecturer.LecturerInScientificReports.Count > 0)
        //            {
        //                foreach (var scientificReport in lecturer.LecturerInScientificReports)
        //                {
        //                    if (scientificReport.ScientificReport.Time > startTime && scientificReport.ScientificReport.Time < endTime)
        //                    {
        //                        sum += scientificReport.ScientificReport.ScientificReportType.HoursConverted;
        //                    }
        //                }
        //            }
        //            if (lecturer.LecturerInScientificWorks.Count > 0)
        //            {
        //                foreach (var scientificWork in lecturer.LecturerInScientificWorks)
        //                {
        //                    if (scientificWork.ScientificWork.Time > startTime && scientificWork.ScientificWork.Time < endTime)
        //                    {
        //                        sum += scientificWork.ScientificWork.Level.HoursConverted;
        //                    }
        //                }
        //            }
        //            if (lecturer.LecturerInPublishBooks.Count > 0)
        //            {
        //                foreach (var publishBook in lecturer.LecturerInPublishBooks)
        //                {
        //                    if (publishBook.PublishBook.Time > startTime && publishBook.PublishBook.Time < endTime)
        //                    {
        //                        sum += publishBook.PublishBook.BookCategory.HoursConverted;
        //                    }
        //                }
        //            }
        //            if (lecturer.LecturerInOtherScientificWorks.Count > 0)
        //            {
        //                foreach (var otherScientificWork in lecturer.LecturerInOtherScientificWorks)
        //                {
        //                    if (otherScientificWork.OtherScientificWork.Time > startTime && otherScientificWork.OtherScientificWork.Time < endTime)
        //                    {
        //                        sum += otherScientificWork.OtherScientificWork.ClassificationOfScientificWork.HoursConverted;
        //                    }
        //                }
        //            }
        //            if (lecturer.StudyGuides.Count > 0)
        //            {
        //                foreach (var studyGuide in lecturer.StudyGuides)
        //                {
        //                    sum += studyGuide.LevelStudyGuide.HoursConverted;
        //                }
        //            }

        //            lecturer.Total = sum;
        //            await _lecturerRepository.UpdateAsync(lecturer);
        //        }
        //    }
        //}

        //private async Task<Lecturer> ScoreByStage(Guid? id, DateTime? startTime, DateTime? endTime)
        //{
        //    var lecturer = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        //    if (startTime != null && endTime != null)
        //    {
        //        float sum = 0;
        //        if (lecturer.LecturerInScientificReports.Count > 0)
        //        {
        //            foreach (var scientificReport in lecturer.LecturerInScientificReports)
        //            {
        //                if (scientificReport.ScientificReport.Time > startTime && scientificReport.ScientificReport.Time < endTime)
        //                {
        //                    sum += scientificReport.ScientificReport.ScientificReportType.Score;
        //                }
        //            }
        //        }
        //        if (lecturer.LecturerInScientificWorks.Count > 0)
        //        {
        //            foreach (var scientificWork in lecturer.LecturerInScientificWorks)
        //            {
        //                if (scientificWork.ScientificWork.Time > startTime && scientificWork.ScientificWork.Time < endTime)
        //                {
        //                    sum += scientificWork.ScientificWork.Level.Score;
        //                }
        //            }
        //        }
        //        if (lecturer.LecturerInPublishBooks.Count > 0)
        //        {
        //            foreach (var publishBook in lecturer.LecturerInPublishBooks)
        //            {
        //                if (publishBook.PublishBook.Time > startTime && publishBook.PublishBook.Time < endTime)
        //                {
        //                    sum += publishBook.PublishBook.BookCategory.Score;
        //                }
        //            }
        //        }
        //        if (lecturer.LecturerInOtherScientificWorks.Count > 0)
        //        {
        //            foreach (var otherScientificWork in lecturer.LecturerInOtherScientificWorks)
        //            {
        //                if (otherScientificWork.OtherScientificWork.Time > startTime && otherScientificWork.OtherScientificWork.Time < endTime)
        //                {
        //                    sum += otherScientificWork.OtherScientificWork.ClassificationOfScientificWork.Score;
        //                }
        //            }
        //        }
        //        if (lecturer.StudyGuides.Count > 0)
        //        {
        //            foreach (var studyGuide in lecturer.StudyGuides)
        //            {
        //                sum += studyGuide.LevelStudyGuide.Score;
        //            }
        //        }

        //        lecturer.Total = sum;
        //    }
        //    return lecturer;
        //}

        //private async Task TotalHours()
        //{
        //    var list = await GetAll().ToListAsync();
        //    foreach (var lecturer in list)
        //    {
        //        int hour = 0;
        //        if (lecturer.ScientificReports.Count > 0)
        //        {
        //            foreach (var scientificReport in lecturer.ScientificReports)
        //            {
        //                hour += scientificReport.ScientificReportType.HoursConverted;
        //            }
        //        }

        //        if (lecturer.LecturerInScientificWorks.Count > 0)
        //        {
        //            foreach (var scientificWork in lecturer.LecturerInScientificWorks)
        //            {
        //                hour += scientificWork.Level.HoursConverted;
        //            }
        //        }
        //        if (lecturer.PublishBooks.Count > 0)
        //        {
        //            foreach (var publishBook in lecturer.PublishBooks)
        //            {
        //                hour += publishBook.BookCategory.HoursConverted;
        //            }
        //        }
        //        if (lecturer.StudyGuides.Count > 0)
        //        {
        //            foreach (var studyGuide in lecturer.StudyGuides)
        //            {
        //                hour += studyGuide.LevelStudyGuide.HoursConverted;
        //            }
        //        }
        //        if (lecturer.OtherScientificWorks.Count > 0)
        //        {
        //            foreach (var otherScientificWork in lecturer.OtherScientificWorks)
        //            {
        //              hour += otherScientificWork.ClassificationOfScientificWork.HoursConverted;
        //            }
        //        }
        //        lecturer.TotalHour = hour;
        //        await _lecturerRepository.UpdateAsync(lecturer);
        //    }
        //}

        #endregion

        public async Task<List<LecturerViewModel>> GetAllLecturer()
        {
            var list = await GetAll().Select(x => new LecturerViewModel(x)).ToListAsync();
            return list;
        }

        public async Task<PagedList<LecturerViewModel>> ListLecturerAsync(RequestListViewModel requestListViewModel, DateTime? startTime, DateTime? endTime)
        {
            await ScoreAndHourCount();
            await ScoreAndHourByStage(startTime, endTime);
            //await TotalHours();
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
            //var lecturer = await ScoreByStage(id, startTime, endTime);
            //return new LecturerViewModel(lecturer);
          
                var table = await GetAll().FirstAsync(x => x.Id == id);
                return new LecturerViewModel(table);
            
        }

        public async Task<ResponseModel> CreateLecturerAsync(LecturerManageModel lecturerManageModel)
        {
            var lecturer = _mapper.Map<Lecturer>(lecturerManageModel);
            await _lecturerRepository.InsertAsync(lecturer);
            return new ResponseModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new LecturerViewModel(lecturer)
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
            if (lecturer.LecturerInScientificWorks == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no scientific work"
                };
            }
            else
            {
                List<ScientificWorkViewModel> scientificWorks = lecturer.LecturerInScientificWorks.Select(x => new ScientificWorkViewModel(x.ScientificWork)).ToList();
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
            if (lecturer.LecturerInScientificReports == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no scientific report"
                };
            }
            else
            {
                List<ScientificReportViewModel> scientificReports = lecturer.LecturerInScientificReports.Select(x => new ScientificReportViewModel(x.ScientificReport)).ToList();
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
            if (lecturer.LecturerInPublishBooks == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no publishBook"
                };
            }
            else
            {
                List<PublishBookViewModel> publishBooks = lecturer.LecturerInPublishBooks.Select(x => new PublishBookViewModel(x.PublishBook)).ToList();
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
            if (lecturer.LecturerInOtherScientificWorks == null)
            {
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This lecturer has no OtherScientificWork"
                };
            }
            else
            {
                List<OtherScientificWorkViewModel> otherScientificWorks = lecturer.LecturerInOtherScientificWorks.Select(x => new OtherScientificWorkViewModel(x.OtherScientificWork)).ToList();
                return new ResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = otherScientificWorks
                };
            }
        }

    }


}
