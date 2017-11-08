using Library.BusinessLogicLayer.Models;
using System;

namespace Library.PresentationLayer.Web.Models
{
    public class BookRentalModel
    {
        public BookRentalModel() { }
        public BookRentalModel(int userId, int bookId, DateTime rentalDate, DateTime returnDate)
        {
            UserId = userId;
            BookId = bookId;
            RentalDate = rentalDate;
            ReturnDate = returnDate;
        }

        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public static implicit operator BookRental(BookRentalModel brm)
        {
            BookRental rental = new BookRental(brm.UserId, brm.BookId, brm.RentalDate, brm.ReturnDate);

            return rental;
        }

        public static implicit operator BookRentalModel(BookRental br)
        {
            BookRentalModel genre = new BookRentalModel(br.UserId, br.BookId, br.RentalDate, br.ReturnDate);

            return genre;
        }
    }
}