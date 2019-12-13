using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelper;
using System.Linq;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PersonFile = "PersonModels.csv";
        private const string TeamsFile = "TeamModels.csv";

        public PersonModel CreatePerson( PersonModel model )
        {
            List<PersonModel> people = PersonFile.FullFilePath().LoadFile().ConvertToPersonModel();

            int currentId = 1;
            if ( people.Count > 0 )
            {
                currentId = people.OrderByDescending( x => x.Id ).First( ).Id + 1;
            }

            model.Id = currentId;

            people.Add( model );

            people.SaveToPersonFile( PersonFile );

            return model;
        }

        //TODO: wire up CreatePrize for textFiles
        public PrizeModel CreatePrize( PrizeModel model )
        {
            // load text file convert text to list<PrizeModel> find max id add the new record with

            List<PrizeModel> prizes = PrizesFile.FullFilePath( ).LoadFile( ).ConvertToPrizeModel( );

            // find max id
            int currentId = 1;
            if ( prizes.Count > 0 )
            {
                currentId = prizes.OrderByDescending( x => x.Id ).First( ).Id + 1;
            }

            model.Id = currentId;

            // add new model to list with id(max + 1)
            prizes.Add( model );

            // convert prizes to list<string> and save to text file
            prizes.SaveToPrizeFile( PrizesFile );

            return model;
        }

        public TeamModel CreateTeam( TeamModel model )
        {
            List<TeamModel> teams = TeamsFile.FullFilePath().LoadFile().ConvertToTeamModel(PersonFile);

            int currentId = 1;
            if ( teams.Count > 0 )
            {
                currentId = teams.OrderByDescending( x => x.Id ).First( ).Id + 1;
            }

            model.Id = currentId;

            teams.Add( model );

            teams.SaveToTeamFile( TeamsFile );

            return model;
        }

        public List<PersonModel> GetPerson_All( )
        {
            return PersonFile.FullFilePath( ).LoadFile( ).ConvertToPersonModel( );
        }
    }
}