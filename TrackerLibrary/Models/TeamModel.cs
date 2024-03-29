﻿using System;
using System.Collections.Generic;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represent 1 team in a tournament
    /// </summary>
    public class TeamModel : Model
    {
        /// <summary>
        /// Represent team's id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represent name of a team
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Represent Player in a team
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>( );
    }
}