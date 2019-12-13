using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents the tournament
    /// </summary>
    public class TournamentModel : Model
    {
        /// <summary>
        /// Represents tournament name
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Represents tournament entry fee
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// Represent teams to compete in the tournament
        /// </summary>
        public List<TeamModel> EnteredTeam { get; set; } = new List<TeamModel>( );

        /// <summary>
        /// Represent prizes for each place
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>( );

        /// <summary>
        /// Represent the rounds that will be played
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>( );
    }
}