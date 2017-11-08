using Library.BusinessLogicLayer.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.PresentationLayer.Web.Models
{
    public class BookModel
    {
        public BookModel() { }
        public BookModel(string title, int noOfPages, int stockCount, WriterModel writer)
        {
            Title = title;
            NoOfPages = noOfPages;
            StockCount = stockCount;
            Writer = writer;
        }

        public int Id { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 50 characters.")]
        [Required]
        public string Title { get; set; }

        
        [Range(1, int.MaxValue, ErrorMessage = "Number of pages must be more than 0.")]
        [DisplayName("Number of pages")]
        [Required]
        public int NoOfPages { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock count must bre more than 0.")]
        [DisplayName("Stock count")]
        [Required]
        public int StockCount { get; set; }

        [Required]
        public WriterModel Writer { get; set; }

        public string GenresList { get; set; }
        
        public static implicit operator Book(BookModel bm)
        {
            Book book = new Book(bm.Title, bm.NoOfPages, bm.StockCount, bm.Writer)
            {
                Id = bm.Id
            };

            return book;
        }

        public static implicit operator BookModel(Book b)
        {
            BookModel book = new BookModel(b.Title, b.NoOfPages, b.StockCount, b.Writer)
            {
                Id = b.Id
            };

            return book;
        }
    }
}