using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents 1 person
    /// </summary>
    public class PersonModel : Model
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of person
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Primary email address of person
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Primary cell phone number of person
        /// </summary>
        public string CellphoneNumber { get; set; }

        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
    }
}