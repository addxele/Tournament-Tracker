using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents prize of a tournament
    /// </summary>
    public class PrizeModel : Model
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represent index first, last place
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// Represent name of the place
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// Represents fixed amount of prize money
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// Represents amount of prize money by percentage
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel( string placeName , string placeNumber , string prizeAmount , string prizePercentage )
        {
            PlaceName = placeName;

            int placeNumberValue = 0;
            int.TryParse( placeNumber , out placeNumberValue );
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0m;
            decimal.TryParse( prizeAmount , out prizeAmountValue );
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0d;
            double.TryParse( prizePercentage , out prizePercentageValue );
            PrizePercentage = prizePercentageValue;
        }

        public PrizeModel( )
        {
        }
    }
}