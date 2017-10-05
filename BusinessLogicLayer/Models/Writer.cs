using System;
using System.Diagnostics;

namespace Library.BusinessLogicLayer.Models
{
    public class Writer
    {
        private string name, bio;

        public Writer() { }
        public Writer(string name, string bio, int? noOfBooks)
        {
            Name = name;
            Biography = bio;
            NoOfBooks = noOfBooks;
        }

        public int Id { get; set; }
        public int? NoOfBooks { get; set; }
        public string Name
        {
            get
            {
                Debug.Assert(name != null);
                return name;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Name", "Valid name is mandatory!");


                string oldValue = name;
                try
                {
                    name = value;
                }
                catch
                {
                    name = oldValue;
                }
            }
        }

        public string Biography
        {
            get
            {
                Debug.Assert(bio != null);
                return bio;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("bio", "Valid bio is mandatory!");


                string oldValue = bio;
                try
                {
                    bio = value;
                }
                catch
                {
                    bio = oldValue;
                }
            }
        }

    }
}
