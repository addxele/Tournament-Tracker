using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SQLConnector : IDataConnection
    {
        private const string db = "Tournaments";

        /// <summary>
        /// Save Person to the database
        /// </summary>
        /// <param name="model"> </param>
        /// <returns> </returns>
        public PersonModel CreatePerson( PersonModel model )
        {
            using ( IDbConnection connection = new SqlConnection( GlobalConfig.CnnString( db ) ) )
            {
                var p = new DynamicParameters();

                p.Add( "@FirstName" , model.FirstName );
                p.Add( "@LastName" , model.LastName );
                p.Add( "@EmailAddress" , model.EmailAddress );
                p.Add( "@CellphoneNumber" , model.CellphoneNumber );
                p.Add( "@id" , dbType: DbType.Int32 , direction: ParameterDirection.Output );

                connection.Execute( "dbo.spPeople_Insert" , p , commandType: CommandType.StoredProcedure );

                model.Id = p.Get<int>( "@id" );
            }

            return model;
        }

        /// <summary>
        /// Save a new prize to the database
        /// </summary>
        /// <param name="model"> </param>
        /// <returns> return prize information </returns>
        public PrizeModel CreatePrize( PrizeModel model )
        {
            using ( IDbConnection connection = new SqlConnection( GlobalConfig.CnnString( db ) ) )
            {
                var p = new DynamicParameters();

                p.Add( "@PlaceNumber" , model.PlaceNumber );
                p.Add( "@PlaceName" , model.PlaceName );
                p.Add( "@PrizeAmount" , model.PrizeAmount );
                p.Add( "@PrizePercentage" , model.PrizePercentage );
                p.Add( "@id" , 0 , dbType: DbType.Int32 , direction: ParameterDirection.Output );

                connection.Execute( "dbo.spPrizes_Insert" , p , commandType: CommandType.StoredProcedure );

                model.Id = p.Get<int>( "@id" );

                return model;
            }
        }

        public List<PersonModel> GetPerson_All( )
        {
            List<PersonModel> output;

            using ( IDbConnection connection = new SqlConnection( GlobalConfig.CnnString( "Tournaments" ) ) )
            {
                output = new List<PersonModel>( connection.Query<PersonModel>( "dbo.spPeople_GetAll" ) );
            }

            return output;
        }
    }
}