using System;

namespace Library.DataAccessLayer.Models
{
    public class BookRental
    {
        public BookRental() { }

        public BookRental(int userId, int bookId, DateTime rentalDate, DateTime returnDate)
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
    }
}
