using ScientificResearch.Core.Business.Models.BookCategorys;
using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.PublishBooks
{
    
    public class PublishBookViewModel
    {
        public PublishBookViewModel()
        {

        }

        public PublishBookViewModel(PublishBook publishBook) : this()
        {
            if (publishBook != null)
            {
                Id = publishBook.Id;
                Name = publishBook.Name;
                Time = publishBook.Time;
                PlaceOfPublication = publishBook.PlaceOfPublication;
                BookCategory = new BookCategoryViewModel(publishBook.BookCategory);
                Lecturer = new LecturerViewModel(publishBook.Lecturer);
                //User = new UserViewModel(scientificWork.User);
                //Lecturer = new LecturerViewModel;
            }
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string PlaceOfPublication { get; set; }

        public BookCategoryViewModel BookCategory { get; set; }

        public LecturerViewModel Lecturer { get; set; }

        //public UserViewModel User { get; set; }
    }
}
