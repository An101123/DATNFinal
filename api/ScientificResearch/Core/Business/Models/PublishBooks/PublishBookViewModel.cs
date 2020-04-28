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
                Lecturers = GetLecturer(publishBook);
                //User = new UserViewModel(publishBook.User);
                //Lecturer = new LecturerViewModel;
            }
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string PlaceOfPublication { get; set; }

        public BookCategoryViewModel BookCategory { get; set; }

        public List<LecturerViewModel> Lecturers { get; set; }
        private List<LecturerViewModel> GetLecturer(PublishBook publishBook)
        {
            var lecturers = new List<LecturerViewModel>();
            foreach (var lecturerInPublishBook in publishBook.LecturerInPublishBooks)
            {
                lecturers.Add(new LecturerViewModel(lecturerInPublishBook.Lecturer));
            }
            return lecturers;
        }

        //public UserViewModel User { get; set; }
    }
}
