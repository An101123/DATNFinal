using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.ScientificReportTypes
{
    public class ScientificReportTypeViewModel
    {
        public ScientificReportTypeViewModel()
        {

        }

        public ScientificReportTypeViewModel(ScientificReportType scientificReportType) : this()
        {
            if (scientificReportType != null)
            {
                Id = scientificReportType.Id;
                Name = scientificReportType.Name;
                Score = scientificReportType.Score;
                HoursConverted = scientificReportType.HoursConverted;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Score { get; set; }

        public int HoursConverted { get; set; }

    }
}
