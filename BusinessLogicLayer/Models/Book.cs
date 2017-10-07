using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Models
{
    public class Book
    {
        private string title;

        public Book() { }
        public Book(string title, int noOfPages, int stockCount, int writerId)
        {
            Title = title;
            NoOfPages = noOfPages;
            StockCount = stockCount;
            WriterId = writerId;
        }

        public string Title
        {
            get
            {
                Debug.Assert(title != null);
                return title;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("title", "Valid title is mandatory!");

                string oldValue = title;
                try
                {
                    title = value;
                }
                catch
                {
                    title = oldValue;
                }
            }
        }

        public int Id { get; set; }
        public int NoOfPages { get; set; }
        public int StockCount { get; set; }
        public int WriterId { get; set; }
    }
}
