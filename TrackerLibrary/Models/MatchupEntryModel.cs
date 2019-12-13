using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents 1 team in the match up
    /// </summary>
    public class MatchupEntryModel : Model
    {
        /// <summary>
        /// Represents 1 team in the match up
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents score for this team
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the match up that this team came from as the winner
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}