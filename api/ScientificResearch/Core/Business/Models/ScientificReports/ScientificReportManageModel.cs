﻿using ScientificResearch.Core.Business.IoC;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.ScientificReports
{
    public class ScientificReportManageModel : IValidatableObject
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public Guid ScientificReportTypeId { get; set; }

        public List<Guid> LecturerIds { get; set; }

        public DateTime Time { get; set; }


        //public Guid UserId { get; set; }


        public void GetScientificReportFromModel(ScientificReport scientificReport)
        {
            scientificReport.Name = Name;
            scientificReport.Content = Content;
            scientificReport.ScientificReportTypeId = ScientificReportTypeId;
            scientificReport.Time = Time;
            //scientificReport.UserId = UserId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required!", new string[] { "Name" });
            }
            if (ScientificReportTypeId == Guid.Empty)
            {
                yield return new ValidationResult("Scientific Report Type is required!", new string[] { "ScientificReportTypeId" });
            }
            var scientificReportTypeRepository = IoCHelper.GetInstance<IRepository<ScientificReportType>>();
            var scientificReportType = scientificReportTypeRepository.GetAll().FirstOrDefault(x => x.Id == ScientificReportTypeId);
            if (scientificReportType == null)
            {
                yield return new ValidationResult("Scientific Report Type is not found!", new string[] { "ScientificReportTypeId" });
            }

            var lecturerRepository = IoCHelper.GetInstance<IRepository<Lecturer>>();
            if (LecturerIds.Count <= 0)
            {
                yield return new ValidationResult("Lecturer is required!", new string[] { "LecturerIds" });
            }
            foreach (var lecturerId in LecturerIds)
            {
                var lecturer = lecturerRepository.GetAll().FirstOrDefault(x => x.Id == lecturerId);
                if (lecturer == null)
                {
                    yield return new ValidationResult("Lecturer is not found!", new string[] { "lecturerId" });
                }
            }
        }
    }
}
