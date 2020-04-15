using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.BookCategorys
{
    public class BookCategoryViewModel
    {
        public BookCategoryViewModel()
        {

        }

        public BookCategoryViewModel(BookCategory bookCategory) : this()
        {
            if (bookCategory != null)
            {
                Id = bookCategory.Id;
                Name = bookCategory.Name;
                Score = bookCategory.Score;
                HoursConverted = bookCategory.HoursConverted;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Score { get; set; }

        public int HoursConverted { get; set; }

    }
}
